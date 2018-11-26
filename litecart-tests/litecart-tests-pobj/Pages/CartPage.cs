using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace LitecartTestsPObj
{
    public class CartPage : Page
    {
        public CartPage(IWebDriver driver) : base (driver)
        {
        }


        public CartPage Open()
        {
            string cartLink = "http://localhost/litecart/en/checkout";
            if (!driver.Url.Equals(cartLink)) driver.Url = cartLink;

            return this;
        }


        public bool HasMoreThanOneProductInCart()
        {
            return driver.FindElements(By.CssSelector("ul.shortcuts")).Count > 0;
        }


        public CartPage SelectTheFirstItem()
        {
            // Choose the first element available
            // in case when the cart contains a lot of items
            driver.FindElement(By.CssSelector("li.shortcut")).Click();

            return this;
        }


        public CartPage RemoveItemFromCart()
        {
            // Ordered items counter
            By cartItemsQtySelector = By.CssSelector("#order_confirmation-wrapper tr:not(.header) .item");
            int cartItemsCnt = driver.FindElements(cartItemsQtySelector).Count;

            driver.FindElement(By.CssSelector("button[name=remove_cart_item]")).Click();

            wait.Until(driver => driver.FindElements(cartItemsQtySelector).Count == (cartItemsCnt - 1));

            return this;
        }


        public void ReturnToTheHomePage()
        {
            ClickByLinkText("Back");
            //return new MainPage(driver);
        }
    }
}
