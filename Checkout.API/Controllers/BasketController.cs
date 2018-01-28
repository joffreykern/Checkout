using Checkout.API.Shared;
using Checkout.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.API.Controllers
{
    [Controller]
    public class BasketController : Controller
    {
        private readonly CreateBasketCommand _createBasketCommand;
        private readonly DeleteBasketCommand _deleteBasketCommand;
        private readonly BasketQuery _basketQuery;
        private readonly AddItemToBasketCommand _addItemToBasketCommand;
        private readonly UpdateItemQuantityCommand _updateItemQuantityCommand;

        public BasketController(CreateBasketCommand createBasketCommand,
            BasketQuery basketQuery,
            DeleteBasketCommand deleteBasketCommand,
            AddItemToBasketCommand addItemToBasketCommand,
            UpdateItemQuantityCommand updateItemQuantityCommand)
        {
            _createBasketCommand = createBasketCommand;
            _basketQuery = basketQuery;
            _deleteBasketCommand = deleteBasketCommand;
            _addItemToBasketCommand = addItemToBasketCommand;
            _updateItemQuantityCommand = updateItemQuantityCommand;
        }

        /// <summary>
        /// Create basket of a specified consumer.
        /// </summary>
        /// <remarks>If the consumer already got a basket, it drop it</remarks>
        [HttpPost("api/consumers/{consumerId}/baskets")]
        [ProducesResponseType(typeof(BasketResource), 200)]
        public IActionResult PostBasket(string consumerId)
        {
            string basketId = _createBasketCommand.Do(consumerId);
            return GetBasket(consumerId, basketId);
        }

        /// <summary>
        /// Return basket of a specified consumer.
        /// </summary>
        /// <response code="404">ConsumerId or BasketId is not valid</response>
        [HttpGet("api/consumers/{consumerId}/baskets/{basketId}")]
        [ProducesResponseType(typeof(BasketResource), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetBasket(string consumerId,
            string basketId)
        {
            BasketDTO basket = _basketQuery.Get(new BasketId(basketId, consumerId));

            if (basket == null)
            {
                return new NotFoundResult();
            }

            return Ok(new BasketResource(basket));
        }

        /// <summary>
        /// Add item to a consumer's basket
        /// </summary>
        /// <response code="400">ConsumerId or BasketId is not valid</response>
        [HttpPost("api/consumers/{consumerId}/baskets/{basketId}/items")]
        [ProducesResponseType(typeof(BasketResource), 200)]
        [ProducesResponseType(400)]
        public IActionResult PostItemBasket(string consumerId,
            string basketId,
            [FromBody]AddItemResource itemResource)
        {
            BasketId id = new BasketId(basketId, consumerId);
            BasketDTO basket = _basketQuery.Get(id);

            if (basket == null)
            {
                return new BadRequestResult();
            }

            _addItemToBasketCommand.Do(id, itemResource.Reference);

            return GetBasket(consumerId, basketId);
        }

        /// <summary>
        /// Update item quantity to a consumer's basket
        /// </summary>
        /// <remarks>If quantity is set to 0, item is drop</remarks>
        /// <response code="400">ConsumerId or BasketId is not valid</response>
        [HttpPut("api/consumers/{consumerId}/baskets/{basketId}/items/{itemId}")]
        [ProducesResponseType(typeof(BasketResource), 200)]
        [ProducesResponseType(400)]
        public IActionResult PutItemBasket(string consumerId,
            string basketId,
            string itemId,
            [FromBody]UpdateItemQuantityResource resource)
        {
            BasketId id = new BasketId(basketId, consumerId);
            BasketDTO basket = _basketQuery.Get(id);

            if (basket == null)
            {
                return new BadRequestResult();
            }

            _updateItemQuantityCommand.Do(id,
                itemId,
                resource.Quantity);

            return GetBasket(consumerId, basketId);
        }

        /// <summary>
        /// Delete basket of a specified consumer
        /// </summary>
        [HttpDelete("api/consumers/{consumerId}/baskets/{basketId}")]
        [ProducesResponseType(201)]
        public IActionResult DeleteBasket(string consumerId,
            string basketId)
        {
            _deleteBasketCommand.Do(new BasketId(basketId, consumerId));

            return Accepted();
        }
    }
}
