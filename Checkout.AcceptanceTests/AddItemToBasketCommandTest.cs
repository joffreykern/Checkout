using Checkout.Domain;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestStack.BDDfy;

namespace Checkout.AcceptanceTests
{
    [TestClass]
    [Story]
    public class AddItemToBasketCommandTest
    {
        private readonly BasketRepository _basketRepository;
        private BasketId _basketId;

        public AddItemToBasketCommandTest()
        {
            _basketRepository = A.Fake<BasketRepository>();
        }

        [TestMethod]
        public void ShouldAddItemToBasket()
        {
            const string productReference = "CheckoutReference";

            this.Given(x => HaveBasket())
                .When(x => AddItemToBasket(productReference))
                .Then(x => ItemAddedToBasket(productReference))
                .BDDfy();
        }

        private void HaveBasket()
        {
            _basketId = new BasketId(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());
        }

        private void AddItemToBasket(string productReference)
        {
            AddItemToBasketCommand command = new AddItemToBasketCommand(_basketRepository);
            command.Do(_basketId, productReference);
        }

        private void ItemAddedToBasket(string productReference)
        {
            A.CallTo(() => _basketRepository.AddItem(_basketId.ConsumerId,
                    _basketId.Id,
                    productReference,
                    1))
                .MustHaveHappened();
        }
    }
}
