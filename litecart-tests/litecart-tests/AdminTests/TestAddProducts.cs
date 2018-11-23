using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LitecartTests
{
    [TestFixture]
    public class TestAddProducts : AdminAuthTestBase
    {
        [Test]
        public void TestAddProduct()
        {
            string productName = TestBase.GetRandomAllowedStringFor(TestBase.GENERAL, 10);
            string productCode = TestBase.rndGenerator.Next(1000, 9999).ToString();
            string productGroup = "Rubber Ducks";
            string productQty = TestBase.rndGenerator.Next(15).ToString();
            string productImageFile = new DirectoryInfo("../../../").FullName + "/images/duck_queen.jpeg";

            ClickByLinkText("Catalog");
            ClickByLinkText("Add New Product");

            /** Set product parameters **/
            /** "General" Tab **/
            driver.FindElement(By.CssSelector("input[type=radio][value='1']")).Click();
            Type(By.CssSelector("[name='name[en]']"), productName);
            Type(By.CssSelector("[name=code]"), productCode);
            SetCheckbox(By.CssSelector("input[type=checkbox][data-name='" + productGroup + "']"));
            SelectDropdownItemByText(By.CssSelector("[name=default_category_id]"), productGroup);
            SetCheckbox(By.CssSelector("input[name='product_groups[]'][value='1-3']"));
            Type(By.CssSelector("[name=quantity]"), Keys.Home + productQty);
            driver.FindElement(By.CssSelector("[type=file]")).SendKeys(productImageFile);
            SelectDropdownItemById(By.CssSelector("[name=sold_out_status_id]"), "2");
            Type(By.CssSelector("[name=date_valid_from]"), "23.11.2017");
            Type(By.CssSelector("[name=date_valid_to]"), "23.11.2020");

            /** "Information" Tab **/
            ClickByLinkText("Information");
            // Wait for tab to load
            wait.Until(driver => driver.FindElement(By.CssSelector("div.trumbowyg-editor")));
            Type(By.CssSelector("[name='short_description[en]']"), "Some short description for " + productName);
            Type(By.CssSelector("div.trumbowyg-editor"), "Some full description for " + productName);
            Type(By.CssSelector("[name='head_title[en]']"), "Title for " + productName);
            Type(By.CssSelector("[name='meta_description[en]']"), "Meta for " + productName);

            /** "Prices" Tab **/
            ClickByLinkText("Prices");
            // Wait for tab to load
            wait.Until(driver => driver.FindElement(By.CssSelector("[name=purchase_price]")));
            Type(By.CssSelector("[name=purchase_price]"), Keys.Home + "5");
            SelectDropdownItemByText(By.CssSelector("[name=purchase_price_currency_code]"), "US Dollars");
            Type(By.CssSelector("[name='gross_prices[USD]']"), Keys.Home + "6");
            Type(By.CssSelector("[name='gross_prices[EUR]']"), Keys.Home + "7.5");


            /** Save product **/
            PressButton(By.CssSelector("button[name=save]"));

            /** Check if product was edded **/
            Assert.AreEqual(driver.FindElement(By.CssSelector("table.dataTable")).FindElements(By.LinkText(productName)).Count, 1);
        }


        protected void Type(By locator, string text)
        {
            if (text != null)
            {
                driver.FindElement(locator).Clear();
                driver.FindElement(locator).SendKeys(text);
            }
        }


        protected void SelectDropdownItemByText(By locator, string text)
        {
            if (text != null && text != "")
            {
                new SelectElement(driver.FindElement(locator)).SelectByText(text);
            }
        }


        protected void SelectDropdownItemById(By locator, string id)
        {
            if (id != null && id != "")
            {
                new SelectElement(driver.FindElement(locator)).SelectByValue(id);
            }
        }


        protected void SetCheckbox(By locator)
        {
            driver.FindElement(locator).Click();
        }


        protected void PressButton(By locator)
        {
            driver.FindElement(locator).Click();
        }


        protected void ClickByLinkText(string valueToFind)
        {
            driver.FindElement(By.PartialLinkText(valueToFind)).Click();
        }
    }
}
