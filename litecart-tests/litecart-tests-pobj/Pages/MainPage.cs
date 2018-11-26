using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace LitecartTestsPObj
{
    public class MainPage : Page
    {
        [FindsBy(How = How.CssSelector, Using = "div.content li[class *= product]")]
        IList<IWebElement> productList;


        public MainPage(IWebDriver driver) : base(driver)
        {
        }


        public MainPage Open()
        {
            driver.Url = "http://localhost/litecart";

            return this;
        }


        public ProductPage SelectFirstProduct()
        {
            if (productList.Count > 0)
            {
                productList[0].Click();
            }else
            {
                throw new Exception("No product items available.");
            }

            return new ProductPage(driver);
        }


        public ProductPage OpenProduct(IWebElement productItem)
        {
            productItem.Click();
            return new ProductPage(driver);
        }
    }
}
