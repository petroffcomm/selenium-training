using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace LitecartTests
{
    [TestFixture]
    public class TestLogin
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.BrowserExecutableLocation = "/Applications/Firefox 2.app/Contents/MacOS/firefox";
            firefoxOptions.UseLegacyImplementation = true;

            driver = new FirefoxDriver(firefoxOptions);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        [Test]
        public void TestLoginAsAdmin()
        {
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
