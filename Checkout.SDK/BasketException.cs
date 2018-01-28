using System;
using System.Net;

namespace Checkout.SDK
{
    public class BasketException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string Content { get; private set; }

        public BasketException(HttpStatusCode statusCode,
            string content)
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}
