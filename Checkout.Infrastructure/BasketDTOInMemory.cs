using Checkout.Domain;
using System.Collections.Generic;

namespace Checkout.Infrastructure
{
    public class BasketDTOInMemory : BasketDTO
    {
        public BasketDTOInMemory(string id,
            string consumerId)
        {
            Id = id;
            ConsumerId = consumerId;
            Items = new List<BasketItemDTO>();
        }

        public string Id { get; private set; }
        public string ConsumerId { get; private set; }
        public List<BasketItemDTO> Items { get; private set; }
    }
}
