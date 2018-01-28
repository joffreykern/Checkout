using System.Runtime.Serialization;

namespace Checkout.API.Shared
{
    [DataContract]
    public class ItemResource
    {
        public ItemResource()
        {

        }

        public ItemResource(string productReference, int quantity)
        {
            Reference = productReference;
            Quantity = quantity;
        }

        [DataMember(Name = "reference")]
        public string Reference { get; set; }

        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }
    }
}