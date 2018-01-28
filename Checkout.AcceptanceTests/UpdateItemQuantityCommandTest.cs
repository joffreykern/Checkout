using Checkout.Domain;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestStack.BDDfy;

namespace Checkout.AcceptanceTests
{
    [TestClass]
    [Story]
    public class UpdateItemQuantityCommandTest
    {
        private readonly BasketRepository _basketRepository;
        private BasketId _basketId;
        private string _itemReference;

        public UpdateItemQuantityCommandTest()
        {
            _basketRepository = A.Fake<BasketRepository>();
        }

        [TestMethod]
        public void ShouldDeleteItemIfUpdateQuantityToZero()
        {
            this.Given(x => ConsumerHasBasketWithItems())
                .When(x => UpdateItemQuantityTo(0))
                .Then(x => ItemIsRemoved())
                .BDDfy();
        }

        [TestMethod]
        public void ShouldUpdateQuantityItem()
        {
            const int newItemQuantity = 3;

            this.Given(x => ConsumerHasBasketWithItems())
                .When(x => UpdateItemQuantityTo(newItemQuantity))
                .Then(x => ItemQuantitySetTo(newItemQuantity))
                .BDDfy();
        }

        private void ItemQuantitySetTo(int newItemQuantity)
        {
            A.CallTo(() => _basketRepository.UpdateItem(
                _basketId.Id,
                _itemReference,
                newItemQuantity))
                .MustHaveHappened();
        }

        private void ConsumerHasBasketWithItems()
        {
            _basketId = new BasketId(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());
            _itemReference = Guid.NewGuid().ToString();
        }

        private void UpdateItemQuantityTo(int quantity)
        {
            UpdateItemQuantityCommand command = new UpdateItemQuantityCommand(_basketRepository);
            command.Do(_basketId,
                _itemReference,
                quantity);
        }

        private void ItemIsRemoved()
        {
            A.CallTo(() => _basketRepository.DeleteItem(_basketId.Id, _itemReference))
                .MustHaveHappened();
        }
    }
}
