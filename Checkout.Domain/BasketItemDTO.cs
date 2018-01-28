namespace Checkout.Domain
{
    public interface BasketItemDTO
    {

        string ProductReference { get; }
        int Quantity { get; }
    }
}
