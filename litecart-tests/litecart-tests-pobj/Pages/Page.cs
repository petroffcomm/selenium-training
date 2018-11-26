using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace LitecartTestsPObj
{
    public class Page
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public Page(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            PageFactory.InitElements(driver, this);
        }


        public void ClickByLinkText(string valueToFind)
        {
            driver.FindElement(By.PartialLinkText(valueToFind)).Click();
        }
    }
}
