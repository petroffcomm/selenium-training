using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LitecartTests
{
    [TestFixture]
    public class TestCountriesSorting : TestBase
    {
        [Test]
        public void TestSortingForCountries()
        {
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";

            List<string> countriesFromPage = new List<string>();
            Dictionary<string, string> countriesWithZones = new Dictionary<string, string>();


            ICollection<IWebElement> countryRows = driver.FindElements(By.CssSelector("[class=dataTable] [class=row]"));
            foreach (IWebElement row in countryRows)
            {
                string countryName = row.FindElement(By.XPath("(.//td)[5]/a")).GetAttribute("textContent");
                countriesFromPage.Add(countryName);

                string zonesCount = row.FindElement(By.XPath("(.//td)[6]")).GetAttribute("textContent").Trim();
                if (!zonesCount.Equals("0") && !zonesCount.Equals("") && !zonesCount.Equals(null))
                {
                    string countryLink = row.FindElement(By.XPath("(.//td)[5]/a")).GetAttribute("href");
                    countriesWithZones.Add(countryName, countryLink);
                }
            }

            List<string> countriesSorted = new List<string>(countriesFromPage);
            countriesSorted.Sort();
            Assert.AreEqual(countriesSorted, countriesFromPage);


            // Start zones checking
            foreach (KeyValuePair<string, string> entry in countriesWithZones)
            {
                driver.Url = entry.Value;
                List<string> zonesFromPage = new List<string>();

                //Choose grandparents for "Remove" buttons
                ICollection<IWebElement> zoneRows = driver.FindElements(By.XPath("//*[@title='Remove']/../.."));
                foreach (IWebElement row in zoneRows)
                {
                    string zoneName = row.FindElement(By.XPath("(.//td)[3]")).GetAttribute("textContent");
                    zonesFromPage.Add(zoneName);
                }

                List<string> zonesSorted = new List<string>(zonesFromPage);
                zonesFromPage.Sort();
                Assert.AreEqual(zonesSorted, zonesFromPage, "Wrong zones sorting for country: {0}", entry.Key);
            }
        }


        [Test]
        public void TestSortingForGeoZones()
        {
            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";

            Dictionary<string, string> countriesWithZones = new Dictionary<string, string>();


            ICollection<IWebElement> countryRows = driver.FindElements(By.CssSelector("[class=dataTable] [class=row]"));
            foreach (IWebElement row in countryRows)
            {
                string countryName = row.FindElement(By.XPath("(.//td)[3]/a")).GetAttribute("textContent");
                string countryLink = row.FindElement(By.XPath("(.//td)[5]/a")).GetAttribute("href");
                countriesWithZones.Add(countryName, countryLink);
            }


            // Start zones checking
            foreach (KeyValuePair<string, string> entry in countriesWithZones)
            {
                driver.Url = entry.Value;
                List<string> zonesFromPage = new List<string>();

                //Choose grandparents for "Remove" buttons
                ICollection<IWebElement> zoneRows = driver.FindElements(By.XPath("//*[@title='Remove']/../.."));
                foreach (IWebElement row in zoneRows)
                {
                    string zoneName = row.FindElement(By.XPath("(.//td)[3]"))
                                         .FindElement(By.TagName("select"))
                                         .FindElement(By.CssSelector("[selected]"))
                                         .GetAttribute("textContent");
                    zonesFromPage.Add(zoneName);
                }

                List<string> zonesSorted = new List<string>(zonesFromPage);
                zonesFromPage.Sort();
                Assert.AreEqual(zonesSorted, zonesFromPage, "Wrong zones sorting for country: {0}", entry.Key);
            }
        }

    }
}
