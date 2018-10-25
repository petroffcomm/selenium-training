using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SampleSiteTest
{
    [TestFixture]
    public class GibsonSiteAvailable
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
            wait.IgnoreExceptionTypes(typeof(NoSuchWindowException));
        }

        [Test]
        public void FirstTest()
        {
            driver.Url = "http://www.gibson.com/";
            wait.Until(driver => driver.Title.Equals("Gibson"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}