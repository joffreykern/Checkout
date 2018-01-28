using System.Collections.Generic;

namespace Checkout.Domain
{
    public interface BasketDTO
    {
        string Id { get; }
        string ConsumerId { get; }
        List<BasketItemDTO> Items { get; }
    }
}