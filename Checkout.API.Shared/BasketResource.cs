using Checkout.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Checkout.API.Shared
{
    [DataContract(Name = "basket")]
    public class BasketResource
    {
        [DataMember(Name = "id")]
        public string Id { get; private set; }

        [DataMember(Name = "consumerId")]
        public string ConsumerId { get; private set; }

        [DataMember(Name = "items")]
        public List<ItemResource> Items { get; private set; }

        public BasketResource()
        {

        }

        public BasketResource(BasketDTO basket)
        {
            Id = basket.Id;
            ConsumerId = basket.ConsumerId;
            Items = basket.Items.Select(x => new ItemResource(x.ProductReference, x.Quantity)).ToList();
        }
    }
}
