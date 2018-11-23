using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;


namespace LitecartTests
{
    [TestFixture]
    public class TestUserRegistration : TestBase
    {

        [Test]
        public void TestNewUserRegistration()
        {
            string userName = GetRandomAllowedStringFor(GENERAL, 20);
            string userPwd = "user_pwd";
            string userEmail = GetRandomAllowedStringFor(EMAIL, 10);

            // Starting registering a new user
            driver.FindElement(By.LinkText("Create Account")).Click();

            By titleToWaitFor = By.CssSelector("h1.title");
            wait.Until(driver => driver.FindElement(titleToWaitFor).GetAttribute("textContent") == "Create Account");

            driver.FindElement(By.CssSelector("[name=firstname]")).SendKeys(userName);
            driver.FindElement(By.CssSelector("[name=lastname]")).SendKeys("test");
            driver.FindElement(By.CssSelector("[name=address1]")).SendKeys("test addr 1");
            driver.FindElement(By.CssSelector("[name=postcode]")).SendKeys("12345");
            driver.FindElement(By.CssSelector("[name=city]")).SendKeys("TestCity");

            // Fill country
            driver.FindElement(By.CssSelector("[role=combobox]")).Click();
            driver.FindElement(By.CssSelector("[role=textbox]")).Click();
            driver.FindElement(By.CssSelector("input.select2-search__field")).SendKeys("United States");
            driver.FindElement(By.XPath("//li[contains(@class,'select2-results__option') and text() = 'United States']")).Click();

            driver.FindElement(By.CssSelector("[name=email]")).SendKeys(userEmail);
            driver.FindElement(By.CssSelector("[name=phone]")).SendKeys(Keys.Home + "666665535");


            driver.FindElement(By.CssSelector("[name=password]")).SendKeys(userPwd);
            driver.FindElement(By.CssSelector("[name=confirmed_password]")).SendKeys(userPwd);
            // Confirm data entered
            driver.FindElement(By.CssSelector("button[name=create_account]")).Click();
            // Wait for being logged in
            wait.Until(driver => driver.FindElement(By.LinkText("Logout")));
            // Log out
            driver.FindElement(By.LinkText("Logout")).Click();

            // Checking user credentials are usable for logging in
            wait.Until(driver => driver.FindElement(By.Name("login")));
            driver.FindElement(By.Name("email")).SendKeys(userEmail);
            driver.FindElement(By.Name("password")).SendKeys(userPwd);
            driver.FindElement(By.Name("login")).Click();
            // Log out
            wait.Until(driver => driver.FindElement(By.LinkText("Logout")));
            driver.FindElement(By.LinkText("Logout")).Click();
        }
    }
}
