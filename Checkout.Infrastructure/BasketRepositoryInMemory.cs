using Checkout.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Infrastructure
{
    public class BasketRepositoryInMemory : BasketRepository
    {
        private readonly List<BasketDTOInMemory> _baskets = new List<BasketDTOInMemory>();

        public string Create(string consumerId)
        {
            BasketDTOInMemory basket = new BasketDTOInMemory(
                Guid.NewGuid().ToString(),
                consumerId);

            _baskets.Add(basket);

            return basket.Id;
        }

        public BasketDTO Get(string consumerId)
        {
            return _baskets
                .Where(x => x.ConsumerId == consumerId)
                .FirstOrDefault();
        }

        public BasketDTO Get(string consumerId,
            string basketId)
        {
            return _baskets
                .Where(x => x.ConsumerId == consumerId)
                .Where(x => x.Id == basketId)
                .FirstOrDefault();
        }

        public void Delete(string consumerId,
            string basketId)
        {
            BasketDTOInMemory basket = _baskets.Find(x => x.ConsumerId == consumerId
                && x.Id == basketId);

            _baskets.Remove(basket);
        }

        public void AddItem(string consumerId,
            string basketId,
            string productReference,
            int quantity)
        {
            BasketDTOInMemory basket = _baskets.Find(x => x.ConsumerId == consumerId
                && x.Id == basketId);
            BasketItemInMemory item = new BasketItemInMemory(productReference, quantity);

            basket.Items.Add(item);
        }

        public void DeleteItem(string id, string itemReference)
        {
            BasketDTOInMemory basket = _baskets.Find(x => x.Id == id);
            BasketItemDTO item = basket.Items.Find(x => x.ProductReference == itemReference);

            basket.Items.Remove(item);
        }

        public void UpdateItem(string id, string itemReference, int newQuantity)
        {
            BasketDTOInMemory basket = _baskets.Find(x => x.Id == id);
            BasketItemInMemory item = basket.Items.Find(x => x.ProductReference == itemReference) as BasketItemInMemory;

            item.Quantity = newQuantity;
        }
    }
}
