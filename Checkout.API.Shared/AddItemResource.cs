using System.Runtime.Serialization;

namespace Checkout.API.Shared
{
    [DataContract]
    public class AddItemResource
    {
        [DataMember(Name = "reference")]
        public string Reference { get; set; }
    }
}
