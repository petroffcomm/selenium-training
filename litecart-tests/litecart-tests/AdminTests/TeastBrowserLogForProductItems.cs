using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace LitecartTests
{
    [TestFixture]
    public class TeastBrowserLogForProductItems : AdminAuthTestBase
    {
        [Test]
        public void TeastBrowserLogs()
        {
            driver.Url = @"http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";
            wait.Until(driver => driver.FindElement(By.CssSelector("form[name=catalog_form]")));

            IList<string> productLinks = new List<string>
                (
                    from p in driver.FindElements(By.CssSelector("table.dataTable tr.row a[href*='edit_product']:nth-child(1)"))
                    select p.GetAttribute("href")
                );


            foreach(string productLink in productLinks)
            {
                IWebElement elementToWait = null;
                // 1. Load new page
                driver.Url = productLink;

                // Saving browser logs to the variable because they are cleared
                // after being obtained by uisng GetLog()
                var logs = driver.Manage().Logs.GetLog(LogType.Browser);
                if (logs.Count > 0)
                {
                    Console.Out.WriteLine("Log records appreared for the '{0}' page.\nContent:", productLink);

                    foreach (LogEntry l in logs)
                    {
                        Console.WriteLine(l);
                    }
                }

                if (productLinks.IndexOf(productLink) > 0)
                {
                    // 2. Check staleness for the 'General' tab
                    wait.Until(ExpectedConditions.StalenessOf(elementToWait));
                }

                // 3. Get another instance of the 'General' tab to check for
                // staleness after the next page from our list is loaded
                elementToWait = driver.FindElement(By.CssSelector("a[href='#tab-general']"));
            }
        }
    }
}
