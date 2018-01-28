using Checkout.Domain;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestStack.BDDfy;

namespace Checkout.AcceptanceTests
{
    [TestClass]
    [Story]
    public class CreateBasketCommandTest
    {
        private readonly BasketRepository _basketRepository;
        private string _consumerId = Guid.NewGuid().ToString();
        private string _basketId = Guid.NewGuid().ToString();
        private string _commandResult;

        public CreateBasketCommandTest()
        {
            _basketRepository = A.Fake<BasketRepository>();
        }

        [TestMethod]
        public void ShouldCreateBasketIfBasketDoesntExist()
        {
            this.Given(x => ConsumerDoesntHaveBasket())
                .When(x => CreateBasket())
                .Then(x => NewBasketCreated())
                .BDDfy();
        }

        private void NewBasketCreated()
        {
            A.CallTo(() => _basketRepository.Create(_consumerId))
                .MustHaveHappened();
        }

        private void ConsumerDoesntHaveBasket()
        {
            A.CallTo(() => _basketRepository.Get(_consumerId))
                .Returns(null);
        }

        [TestMethod]
        public void ShouldReturnExistingBasketIfConsumerAlreadyGotOne()
        {
            this.Given(x => ConsumerHasBasket())
                .When(x => CreateBasket())
                .Then(x => GetExistingBasket())
                .BDDfy();
        }

        private void GetExistingBasket()
        {
            A.CallTo(() => _basketRepository.Create(_consumerId))
                .MustNotHaveHappened();

            Assert.AreEqual(_commandResult, _basketId);
        }

        private void ConsumerHasBasket()
        {
            BasketDTO basket = A.Fake<BasketDTO>();
            A.CallTo(() => basket.Id)
                .Returns(_basketId);

            A.CallTo(() => _basketRepository.Get(_consumerId))
                .Returns(basket);
        }

        private void CreateBasket()
        {
            CreateBasketCommand command = new CreateBasketCommand(_basketRepository);
            _commandResult = command.Do(_consumerId);
        }
    }
}
