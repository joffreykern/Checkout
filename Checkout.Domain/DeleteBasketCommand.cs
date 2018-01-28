namespace Checkout.Domain
{
    public class DeleteBasketCommand
    {
        private readonly BasketRepository _basketRepository;

        public DeleteBasketCommand(BasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public void Do(BasketId basketId)
        {
            BasketDTO basket = _basketRepository.Get(basketId.ConsumerId, basketId.Id);

            if (basket != null)
            {
                _basketRepository.Delete(basketId.ConsumerId, basketId.Id);
            }
        }
    }
}
