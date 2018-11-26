using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace LitecartTests
{
    public class AdminAuthTestBase
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("start-fullscreen");
            chromeOptions.AddArgument("disable-infobars");
            chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
            driver = new ChromeDriver(chromeOptions);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));


            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            By elementToWaitFor = By.XPath("//a[contains(@href, 'logout.php') and @title='Logout']");
            wait.Until(driver => driver.FindElement(elementToWaitFor));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
