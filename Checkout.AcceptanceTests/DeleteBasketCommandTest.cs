using Checkout.Domain;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestStack.BDDfy;

namespace Checkout.AcceptanceTests
{
    [Story]
    [TestClass]
    public class DeleteBasketCommandTest
    {
        private string _consumerId = Guid.NewGuid().ToString();
        private string _basketId = Guid.NewGuid().ToString();
        private readonly BasketRepository _basketRepository;

        public DeleteBasketCommandTest()
        {
            _basketRepository = A.Fake<BasketRepository>();
        }

        [TestMethod]
        public void ShouldBeAbleToDeleteAnExistingBasket()
        {
            this.Given(x => GetConsumerBasketReturns(A.Fake<BasketDTO>()))
                .When(x => DeleteBasket())
                .Then(x => BasketIsDeleted())
                .BDDfy();
        }

        [TestMethod]
        public void ShouldNotTryToDeleteBasketIfItDoesntExist()
        {
            this.Given(x => GetConsumerBasketReturns(null))
                .When(x => DeleteBasket())
                .Then(x => DidNotTryToDeleteTheBasket())
                .BDDfy();
        }

        private void GetConsumerBasketReturns(BasketDTO basket)
        {
            A.CallTo(() => _basketRepository.Get(_consumerId, _basketId))
                .Returns(basket);
        }

        private void DeleteBasket()
        {
            DeleteBasketCommand command = new DeleteBasketCommand(_basketRepository);
            command.Do(new BasketId(_basketId, _consumerId));
        }

        private void BasketIsDeleted()
        {
            A.CallTo(() => _basketRepository.Delete(_consumerId, _basketId))
                .MustHaveHappened();
        }

        private void DidNotTryToDeleteTheBasket()
        {
            A.CallTo(() => _basketRepository.Delete(_consumerId, _basketId))
                .MustNotHaveHappened();
        }
    }
}
