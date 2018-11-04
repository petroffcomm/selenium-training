using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace LitecartTests
{
    [TestFixture]
    public class ProductStickerTests
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

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Url = "http://localhost/litecart";
        }

        [Test]
        public void TestProductHasSingleSticker()
        {
            List<IWebElement> products = new List<IWebElement>(
                driver.FindElements(
                    By.CssSelector("li[class *= product]")
                )
            );

            foreach(IWebElement product in products)
            {
                int stickersCount = product.FindElements(
                    By.CssSelector("div[class *= sticker]")).Count;
                string title = product.FindElement(By.CssSelector("a.link")).GetAttribute("title");

                Assert.AreEqual(stickersCount, 1, "[{0}] product has {1} stickers", title, stickersCount);
            }

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
