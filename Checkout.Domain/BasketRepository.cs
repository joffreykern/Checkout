namespace Checkout.Domain
{
    public interface BasketRepository : Repository<BasketDTO>
    {
        BasketDTO Get(string consumerId);
        BasketDTO Get(string consumerId, string basketId);
        string Create(string consumerId);
        void Delete(string consumerId, string basketId);
        void AddItem(string consumerId,
            string basketId,
            string productReference,
            int quantity);
        void DeleteItem(string id, string itemReference);
        void UpdateItem(string id, string itemReference, int newItemQuantity);
    }
}
