namespace Checkout.Domain
{
    public class BasketId
    {
        public string Id { get; private set; }
        public string ConsumerId { get; private set; }

        public BasketId(string id,
            string consumerId)
        {
            Id = id;
            ConsumerId = consumerId;
        }
    }
}
