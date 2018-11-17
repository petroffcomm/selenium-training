using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NFluent;
using System.Text.RegularExpressions;

namespace LitecartTests
{
    [TestFixture]
    public class TestProductPage
    {
        private IWebDriver driver;


        [SetUp]
        public void SetUp()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(4);
            driver.Url = "http://localhost/litecart";
        }


        [Test]
        public void TestProductDetails()
        {
            By regPriceSelector = By.CssSelector("s.regular-price");
            By campaignPriceSelector = By.CssSelector("strong.campaign-price");

            IWebElement productItemOnMainPage = driver.FindElement(By.CssSelector("#box-campaigns li[class *= product]"));
            // Get product parameters from the main page
            string nameMainPage = productItemOnMainPage.FindElement(By.CssSelector("div.name")).GetAttribute("textContent");
            // Selectors chosen are used as an implicit partial style check
            int regPriceMainPage = ParsePriceFor(productItemOnMainPage.FindElement(regPriceSelector));
            int cmpgnPriceMainPage = ParsePriceFor(productItemOnMainPage.FindElement(campaignPriceSelector));

            double regPriceMainPageFontSize = GetFontSizeFor(productItemOnMainPage.FindElement(regPriceSelector));
            double cmpgnPriceMainPageFontSize = GetFontSizeFor(productItemOnMainPage.FindElement(campaignPriceSelector));

            Dictionary<string, int> regPriceMainPageRGB = GetRGBParams(productItemOnMainPage.FindElement(regPriceSelector));
            Dictionary<string, int> cmpgnPriceMainPageRGB = GetRGBParams(productItemOnMainPage.FindElement(campaignPriceSelector));

            Check.That(cmpgnPriceMainPageFontSize).IsStrictlyGreaterThan(regPriceMainPageFontSize);
            Assert.True(CheckIfColorIs("grey", regPriceMainPageRGB));
            Assert.True(CheckIfColorIs("red", cmpgnPriceMainPageRGB));


            // Open product details
            driver.Url = productItemOnMainPage.FindElement(By.CssSelector("a[class=link]")).GetAttribute("href");
            // Get product parameters from the detailed view
            string nameDetailsPage = driver.FindElement(By.CssSelector("h1.title")).GetAttribute("textContent");
            // Selectors chosen are used as an implicit partial style check
            int regPriceDetailsPage = ParsePriceFor(driver.FindElement(regPriceSelector));
            int cmpgnPriceDetailsPage = ParsePriceFor(driver.FindElement(campaignPriceSelector));

            double regPriceDetailsPageFontSize = GetFontSizeFor(driver.FindElement(regPriceSelector));
            double cmpgnPriceDetailsPageFontSize = GetFontSizeFor(driver.FindElement(campaignPriceSelector));

            Dictionary<string, int> regPriceDetailsPageRGB = GetRGBParams(driver.FindElement(regPriceSelector));
            Dictionary<string, int> cmpgnPriceDetailsPageRGB = GetRGBParams(driver.FindElement(campaignPriceSelector));


            Check.That(cmpgnPriceDetailsPageFontSize).IsStrictlyGreaterThan(regPriceDetailsPageFontSize);
            Assert.True(CheckIfColorIs("grey", regPriceDetailsPageRGB));
            Assert.True(CheckIfColorIs("red", cmpgnPriceDetailsPageRGB));

            Assert.AreEqual(nameMainPage, nameDetailsPage);
            Assert.AreEqual(regPriceMainPage, regPriceDetailsPage);
            Assert.AreEqual(cmpgnPriceMainPage, cmpgnPriceDetailsPage);
        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        /** Additional helper-methods **/
        private int ParsePriceFor(IWebElement webElement)
        {
            return Int32.Parse(webElement.GetAttribute("textContent").Replace('$', ' '));
        }


        private double GetFontSizeFor(IWebElement webElement)
        {
            string text = webElement.GetCssValue("font-size");
            text = text.Remove(text.IndexOf("px"));
            return Double.Parse(text);
        }


        private Dictionary<string, int> GetRGBParams(IWebElement webElement)
        {
            string text = webElement.GetCssValue("color");

            string pattern = "(\\d+)";
            Regex rgx = new Regex(pattern);
            MatchCollection matches = rgx.Matches(text);

            Dictionary<string, int> dict = new Dictionary<string, int>
            {
                { "R", Int32.Parse(matches[0].Value) },
                { "G", Int32.Parse(matches[1].Value) },
                { "B", Int32.Parse(matches[2].Value) }
            };

            return dict;
        }


        private bool CheckIfColorIs(string colorName, Dictionary<string, int> rgbDict)
        {
            switch (colorName)
            {
                case "grey":
                    return rgbDict["R"].Equals(rgbDict["G"]) && rgbDict["R"].Equals(rgbDict["B"]);
                case "red":
                    return rgbDict["G"].Equals(rgbDict["B"]);
            }
            return true;
        }
    }
}

