using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace LitecartTestsPObj
{
    public class ApplicationManager
    {
        IWebDriver driver;

        MainPage mainPage;
        ProductPage productPage;
        CartPage cartPage;


        public ApplicationManager()
        {
            driver = new FirefoxDriver();

            mainPage = new MainPage(driver);
            productPage = new ProductPage(driver);
            cartPage = new CartPage(driver);
        }


        public void Quit()
        {
            driver.Quit();
        }


        public void AddItemsToCart(int itemsQty)
        {
            for (int i = 1; i <= 3; i++)
            {
                mainPage.Open()
                        .SelectFirstProduct()
                        .AddProductToCart()
                        .AndWaitForItemToBeAdded(i)
                        .ReturnToTheHomePage();
            }

        }


        public void ClearCart()
        {
            cartPage.Open();

            if (cartPage.HasMoreThanOneProductInCart())
            {
                cartPage.SelectTheFirstItem();
                cartPage.RemoveItemFromCart();
                ClearCart();
            }
            else
            {
                cartPage.RemoveItemFromCart();
                cartPage.ReturnToTheHomePage();
            }
        }
    }
}