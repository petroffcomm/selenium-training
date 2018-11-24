using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace LitecartTests
{
    [TestFixture]
    public class TestCartActions : TestBase
    {
        [Test]
        public void TestCart()
        {
            AddItemsToCart(3);
            ClearCart();

            string cartItemsQty = driver.FindElement(By.CssSelector("span.quantity"))
                                        .GetAttribute("textContent");
            Assert.AreEqual(cartItemsQty, "0");
        }


        public void AddItemsToCart(int itemsQty)
        {
            for (int i = 1; i <= 3; i++)
            {
                OpenProductItem();
                AddItemToCart();
                WaitForItemToBeAdded(i);
                ClickByLinkText("Home");
            }
            
        }


        protected void OpenProductItem()
        {
            driver.FindElement(By.CssSelector("div.content li[class *= product]")).Click();
        }


        protected void AddItemToCart()
        {
            /** Check if size selector is present and set proper size**/
            By locator = By.CssSelector("[name='options[Size]']");

            if (driver.FindElements(locator).Count == 1)
            {
                new SelectElement(driver.FindElement(locator)).SelectByValue("Small");
            }

            driver.FindElement(By.CssSelector("button[name=add_cart_product]")).Click();
        }


        void WaitForItemToBeAdded(int expectedItemsQty)
        {
            By cartItemsQtySelector = By.CssSelector("span.quantity");

            wait.Until(driver => driver.FindElement(cartItemsQtySelector)
                       .GetAttribute("textContent") == expectedItemsQty.ToString());
        }


        public void ClearCart()
        {
            string cartLink = "http://localhost/litecart/en/checkout";

            if (!driver.Url.Equals(cartLink)) driver.Url = cartLink;

            if (driver.FindElements(By.CssSelector("ul.shortcuts")).Count == 1)
            {
                // Choose first element available
                driver.FindElement(By.CssSelector("li.shortcut")).Click();
                RemoveItemFromCart();
                ClearCart();
            }
            else
            {
                RemoveItemFromCart();
                ClickByLinkText("Back");
            }
        }


        protected void RemoveItemFromCart()
        {
            // Ordered items counter
            By cartItemsQtySelector = By.CssSelector("#order_confirmation-wrapper tr:not(.header) .item");
            int cartItemsCnt = driver.FindElements(cartItemsQtySelector).Count;

            driver.FindElement(By.CssSelector("button[name=remove_cart_item]")).Click();

            wait.Until( driver => driver.FindElements(cartItemsQtySelector).Count == (cartItemsCnt - 1) );
        }


        protected void ClickByLinkText(string valueToFind)
        {
            driver.FindElement(By.PartialLinkText(valueToFind)).Click();
        }
    }
}
