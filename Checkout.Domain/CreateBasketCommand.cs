namespace Checkout.Domain
{
    public class CreateBasketCommand
    {
        private readonly BasketRepository _basketRepository;

        public CreateBasketCommand(BasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public string Do(string consumerId)
        {
            BasketDTO basket = _basketRepository.Get(consumerId);

            if (basket != null)
            {
                return basket.Id;
            }

            return _basketRepository.Create(consumerId);
        }
    }
}
