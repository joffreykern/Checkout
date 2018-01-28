using Checkout.Domain;

namespace Checkout.Infrastructure
{
    public class BasketItemInMemory : BasketItemDTO
    {
        public BasketItemInMemory(string productReference, int quantity)
        {
            ProductReference = productReference;
            Quantity = quantity;
        }

        public string ProductReference { get; }

        public int Quantity { get; set; }
    }
}