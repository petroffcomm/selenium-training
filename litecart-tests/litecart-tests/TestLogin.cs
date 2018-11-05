using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;
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
            string browser = "ff";
            switch (browser)
            {
                case "ff":
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    //firefoxOptions.BrowserExecutableLocation = "/Applications/Firefox 2.app/Contents/MacOS/firefox";
                    firefoxOptions.BrowserExecutableLocation = "/Applications/Firefox Nightly.app/Contents/MacOS/firefox";
                    firefoxOptions.UseLegacyImplementation = false;

                    driver = new FirefoxDriver(firefoxOptions);
                    //driver = new FirefoxDriver();
                    break;
                case "chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("start-fullscreen");
                    chromeOptions.AddArgument("disable-infobars");
                    driver = new ChromeDriver(chromeOptions);
                    break;
                case "safari":
                    driver = new SafariDriver();
                    break;
            }

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            // Show browser capabilities
            Console.Out.WriteLine( ((IHasCapabilities)driver).Capabilities);
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
            // Show cookies
            Console.Out.WriteLine("Cookies sent by site:");
            foreach (Cookie cookie in driver.Manage().Cookies.AllCookies)
            {
                Console.Out.WriteLine("{0} = {1}", cookie.Name, cookie.Value);
            }

            driver.Quit();
        }
    }
}
