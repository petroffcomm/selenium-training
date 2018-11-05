using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace LitecartTests
{
    [TestFixture]
    public class AdminPanelTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("start-fullscreen");
            chromeOptions.AddArgument("disable-infobars");
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

        [Test]
        public void TestHeadersOnAdminPanelSections()
        {
            By sectionSelector = By.CssSelector("td#sidebar li#app-");
            // In case when a section has subsections, the 1-st one is opened
            // by default. So, it makes no sence to take it in account.
            By subSectionSelector = By.CssSelector("li:not([class=selected])");
            By headerSelector = By.CssSelector("td#content h1");

            // Start walking through top-level sections
            int sectionsCount = driver.FindElements(sectionSelector).Count;
            for (int currPos = 0; currPos < sectionsCount; currPos++)
            {
                IWebElement section = driver.FindElements(sectionSelector)[currPos];
                section.Click();

                Assert.IsTrue( driver.FindElements(headerSelector).Count == 1,
                              "Header wasn't found for section [{0}]", currPos);
                              
                // Start walking through subsections
                int subSectionsCount = driver.FindElements(sectionSelector)[currPos]
                                             .FindElements(subSectionSelector)
                                             .Count;
                if (subSectionsCount != 0)
                {
                    for(int currSubPos = 0; currSubPos < subSectionsCount; currSubPos++)
                    {
                        IWebElement subSection = driver.FindElements(sectionSelector)[currPos]
                                                       .FindElements(subSectionSelector)[currSubPos];
                        subSection.Click();

                        Assert.IsTrue(driver.FindElements(headerSelector).Count == 1,
                                      "Header wasn't found for section [{0}], subsection [{1}]", currPos, currSubPos);
                    }
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
