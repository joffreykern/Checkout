namespace Checkout.Domain
{
    public class BasketQuery
    {
        private readonly BasketRepository _basketRepository;

        public BasketQuery(BasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public BasketDTO Get(BasketId basketId)
        {
            return _basketRepository.Get(basketId.ConsumerId,
                basketId.Id);
        }
    }
}
