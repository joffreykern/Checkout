namespace Checkout.Domain
{
    public class AddItemToBasketCommand
    {
        private readonly BasketRepository _basketRepository;

        public AddItemToBasketCommand(BasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public void Do(BasketId basketId,
            string productReference)
        {
            _basketRepository.AddItem(basketId.ConsumerId,
                basketId.Id,
                productReference,
                1);
        }
    }
}
