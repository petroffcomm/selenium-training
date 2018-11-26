using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace LitecartTestsPObj
{
    public class ProductPage : Page
    {
        public ProductPage(IWebDriver driver) : base (driver)
        {
        }

        public void Open()
        {
            //driver.Url = "";
        }


        public ProductPage AddProductToCart()
        {
            /** Check if size selector is present and set proper size**/
            By locator = By.CssSelector("[name='options[Size]']");

            if (driver.FindElements(locator).Count == 1)
            {
                new SelectElement(driver.FindElement(locator)).SelectByValue("Small");
            }

            driver.FindElement(By.CssSelector("button[name=add_cart_product]")).Click();

            return this;
        }


        public ProductPage AndWaitForItemToBeAdded(int expectedItemsQty)
        {
            By cartItemsQtySelector = By.CssSelector("span.quantity");

            wait.Until(driver => driver.FindElement(cartItemsQtySelector)
                       .GetAttribute("textContent") == expectedItemsQty.ToString());

            return this;
        }

        public void ReturnToTheHomePage()
        {
            ClickByLinkText("Home");
            //return new MainPage(driver);
        }
    }
}
