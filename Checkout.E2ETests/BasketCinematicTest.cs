using Checkout.API.Shared;
using Checkout.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TestStack.BDDfy;

namespace Checkout.E2ETests
{
    [TestClass]
    [Story]
    public class BasketCinematicTest
    {
        private readonly Basket _basket;
        private BasketResource _basketResource;

        public BasketCinematicTest()
        {
            _basket = new Basket("http://localhost:1337");
        }

        [TestMethod]
        public void ShouldBeAbleToCreateGetAndDeleteBasket()
        {
            string consumerId = Guid.NewGuid().ToString();

            this.Given(x => CreateBasketForConsumer(consumerId))
                    .And(x => AssertGetBasket(consumerId))
                .When(x => GetBasketOfConsumer(consumerId))
                    .And(x => AssertGetBasket(consumerId))
                .Then(x => DeleteBasketOfConsumer(consumerId))
                .But(x => GetBasketOfConsumer(consumerId))
                    .Then(x => AssertBasketDoestExist())
                .BDDfy();
        }
        private void AssertGetBasket(string consumerId)
        {
            Assert.IsNotNull(_basketResource);
            Assert.AreEqual(_basketResource.ConsumerId, consumerId);
        }

        private void AssertBasketDoestExist()
        {
            Assert.IsNull(_basketResource);
        }

        [TestMethod]
        public void ShouldBeAbleToManageBasketItems()
        {
            string consumerId = Guid.NewGuid().ToString();
            const string productReference = "CheckoutReference";

            this.Given(x => CreateBasketForConsumer(consumerId))
                .When(x => AddItemToBasket(productReference, consumerId))
                .Then(x => GetBasketOfConsumer(consumerId))
                    .And(x => BasketContainMyItemWithReference(productReference, consumerId))
                .BDDfy();
        }

        [TestMethod]
        public void ShouldBeAbleToModifyItemQuantityAndDropIt()
        {
            string consumerId = Guid.NewGuid().ToString();
            const string productReference = "CheckoutReference";
            const int newQuantity = 2;

            this.Given(x => CreateBasketForConsumer(consumerId))
                .When(x => AddItemToBasket(productReference, consumerId))
                .Then(x => UpdateItemQuantityTo(newQuantity, productReference, consumerId))
                    .And(x => QuantityWasUpdated(newQuantity, productReference))
                .Then(x => UpdateItemQuantityTo(0, productReference, consumerId))
                    .And(x => ItemWasRemoved(productReference))
                .BDDfy();
        }

        private void ItemWasRemoved(string productReference)
        {
            Assert.IsTrue(_basketResource.Items.Count(x => x.Reference == productReference) == 0);
        }

        private void QuantityWasUpdated(int newQuantity, string productReference)
        {
            Assert.AreEqual(newQuantity, _basketResource.Items.FirstOrDefault(x => x.Reference == productReference).Quantity);
        }

        private void UpdateItemQuantityTo(int newQuantity, string productReference, string consumerId)
        {
            _basketResource = _basket.UpdateItemQuantity(consumerId,
                _basketResource.Id,
                productReference,
                newQuantity);
        }

        private void BasketContainMyItemWithReference(string productReference,
            string consumerId)
        {
            Assert.AreEqual(_basketResource.Items.First().Reference, productReference);
        }

        private void CreateBasketForConsumer(string consumerId)
        {
            _basketResource = _basket.Create(consumerId);
        }

        private void GetBasketOfConsumer(string consumerId)
        {
            _basketResource = _basket.Get(consumerId, _basketResource.Id);
        }

        private void DeleteBasketOfConsumer(string consumerId)
        {
            _basket.Delete(consumerId, _basketResource.Id);
        }

        private void AddItemToBasket(string productReference,
            string consumerId)
        {
            _basketResource = _basket.CreateItem(consumerId,
                _basketResource.Id,
                productReference);
        }
    }
}
