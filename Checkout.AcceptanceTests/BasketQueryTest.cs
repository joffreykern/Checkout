using Checkout.Domain;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestStack.BDDfy;

namespace Checkout.AcceptanceTests
{
    [Story]
    [TestClass]
    public class BasketQueryTest
    {
        private readonly BasketRepository _basketRepository;
        private string _consumerId = Guid.NewGuid().ToString();
        private string _basketId = Guid.NewGuid().ToString();
        private BasketDTO _returnedBasket;

        public BasketQueryTest()
        {
            _basketRepository = A.Fake<BasketRepository>();
        }

        [TestMethod]
        public void ShouldReturnBasket()
        {
            this.Given(x => ConsumerHasABasket())
                .When(x => GetBasket())
                .Then(x => GetConsumerBasket())
                .BDDfy();
        }

        private void GetConsumerBasket()
        {
            Assert.IsNotNull(_returnedBasket);
            Assert.AreEqual(_consumerId, _returnedBasket.ConsumerId);
            Assert.AreEqual(_basketId, _returnedBasket.Id);
        }

        private void GetBasket()
        {
            BasketQuery query = new BasketQuery(_basketRepository);
            _returnedBasket = query.Get(new BasketId(_basketId, _consumerId));
        }

        private void ConsumerHasABasket()
        {
            A.CallTo(() => _basketRepository.Get(_consumerId, _basketId))
                .Returns(new BasketImpl(_basketId, _consumerId));
        }

        private class BasketImpl : BasketDTO
        {
            public BasketImpl(string id,
                string consumerId)
            {
                Id = id;
                ConsumerId = consumerId;
            }

            public string Id { get; private set; }

            public string ConsumerId { get; private set; }

            public List<BasketItemDTO> Items => throw new NotImplementedException();
        }
    }
}
