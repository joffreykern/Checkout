namespace Checkout.Domain
{
    public class UpdateItemQuantityCommand
    {
        private readonly BasketRepository _basketRepository;

        public UpdateItemQuantityCommand(BasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public void Do(BasketId basketId,
            string itemReference,
            int quantity)
        {
            if (quantity <= 0)
            {
                _basketRepository.DeleteItem(basketId.Id,
                    itemReference);
            }
            else
            {
                _basketRepository.UpdateItem(basketId.Id,
                    itemReference,
                    quantity);
            }
        }
    }
}
