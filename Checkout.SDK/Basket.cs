using Checkout.API.Shared;
using RestSharp;
using System.Net;

namespace Checkout.SDK
{
    public class Basket
    {
        private readonly RestClient _client;

        public Basket(string endpoint)
        {
            _client = new RestClient(endpoint);
        }

        public BasketResource Get(string consumerId,
            string basketId)
        {
            RestRequest request = new RestRequest(
                $"/api/consumers/{consumerId}/baskets/{basketId}",
                Method.GET);

            IRestResponse<BasketResource> response = _client.Execute<BasketResource>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            return response.Data;
        }

        public BasketResource Create(string consumerId)
        {
            RestRequest request = new RestRequest(
                $"/api/consumers/{consumerId}/baskets",
                Method.POST);

            return _client.Execute<BasketResource>(request).Data;
        }

        public BasketResource CreateItem(string consumerId,
            string basketId,
            string productReference)
        {

            RestRequest request = new RestRequest(
                $"/api/consumers/{consumerId}/baskets/{basketId}/items",
                Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new AddItemResource() { Reference = productReference });

            IRestResponse<BasketResource> response = _client.Execute<BasketResource>(request);
            ValidateResponseStatusCodeEqualsTo(HttpStatusCode.OK,
                response);

            return response.Data;
        }

        public BasketResource UpdateItemQuantity(string consumerId,
            string basketId,
            string productReference,
            int quantity)
        {
            RestRequest request = new RestRequest(
                   $"/api/consumers/{consumerId}/baskets/{basketId}/items/{productReference}",
                   Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new UpdateItemQuantityResource() { Quantity = quantity });

            IRestResponse<BasketResource> response = _client.Execute<BasketResource>(request);
            ValidateResponseStatusCodeEqualsTo(HttpStatusCode.OK, response);

            return response.Data;
        }

        public void Delete(string consumerId,
            string basketId)
        {
            RestRequest request = new RestRequest(
                $"/api/consumers/{consumerId}/baskets/{basketId}",
                Method.DELETE);

            IRestResponse response = _client.Execute(request);
            ValidateResponseStatusCodeEqualsTo(HttpStatusCode.Accepted,
                response);
        }

        private static void ValidateResponseStatusCodeEqualsTo(
            HttpStatusCode statusCode,
            IRestResponse response)
        {
            if (response.StatusCode != statusCode)
            {
                throw new BasketException(response.StatusCode,
                    response.Content);
            }
        }
    }
}
