using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace LitecartTestsPObj
{
    [TestFixture]
    public class TestCartActions : TestBase
    {
        [Test]
        public void TestCart()
        {
            app.AddItemsToCart(3);
            app.ClearCart();
        }
    }
}
