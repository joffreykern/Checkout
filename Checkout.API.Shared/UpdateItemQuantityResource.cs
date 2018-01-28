using System.Runtime.Serialization;

namespace Checkout.API.Shared
{
    [DataContract]
    public class UpdateItemQuantityResource
    {
        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }
    }
}
