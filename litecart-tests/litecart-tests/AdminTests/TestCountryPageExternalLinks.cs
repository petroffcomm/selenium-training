using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LitecartTests
{
    [TestFixture]
    public class TestCountryPageExternalLinks : AdminAuthTestBase
    {
        [Test]
        public void TestExternalLinksNotCorrupted()
        {
            ClickByLinkText("Countries");
            wait.Until(driver => driver.FindElement(By.CssSelector("form[name=countries_form]")));
            ClickByLinkText("China");
            wait.Until(driver => driver.FindElement(By.CssSelector("input[value=China]")));

            ICollection<IWebElement> extLinks = driver.FindElements(By.CssSelector(".fa-external-link"));


            string mainWindow = driver.CurrentWindowHandle;
            foreach (IWebElement linkElement in extLinks)
            {
                linkElement.Click();
                wait.Until( driver => driver.WindowHandles.Count > 1 );

                List<string> windows = new List<string>(driver.WindowHandles);
                windows.Remove(mainWindow);

                driver.SwitchTo().Window(windows[0]);
                wait.Until(driver => !driver.Title.Equals(string.Empty));

                driver.Close();
                driver.SwitchTo().Window(mainWindow);
            }
        }


        protected void ClickByLinkText(string valueToFind)
        {
            driver.FindElement(By.PartialLinkText(valueToFind)).Click();
        }
    }
}