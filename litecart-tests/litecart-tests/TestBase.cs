using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace LitecartTests
{
    public class TestBase
    {
        public static Random rndGenerator = new Random();

        protected IWebDriver driver;
        protected WebDriverWait wait;

        public const int GENERAL = 0;
        public const int PHONE = 1;
        public const int EMAIL = 2;


        [SetUp]
        public void SetUp()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(4);
            driver.Url = "http://localhost/litecart";

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }


        /** Data generators**/


        public static string GenerateRandomString(int length)
        {
            int strLenght = Convert.ToInt32(rndGenerator.NextDouble() * length);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i <= strLenght; i++)
            {
                // Generating random char-codes and appending then to the string.
                // Taking into account that printable char-symbols start from 32th code.
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rndGenerator.NextDouble() * 65)));
            }

            return builder.ToString();
        }


        public static String GetRandomAllowedStringFor(int type, int lenght)
        {
            string allowedChars = " abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            string strToReturn = "";

            if (type == GENERAL)
            {
                allowedChars = " _.-abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
                strToReturn = genCharSeq(lenght, allowedChars);
            }

            if (type == PHONE)
            {
                allowedChars = " 0123456789";
                strToReturn = "+" + genCharSeq(lenght, allowedChars);
            }

            if (type == EMAIL)
            {
                allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
                string postfix = "@test.test";

                int prefix_lenght = lenght - postfix.Length;
                if (prefix_lenght <= 0) prefix_lenght = 1;

                strToReturn = genCharSeq(prefix_lenght, allowedChars) + postfix;
            }

            return strToReturn;
        }

        protected static string genCharSeq(int lenght, string charSet)
        {
            StringBuilder builder = new StringBuilder();

            for (var i = 0; i < lenght; i++)
            {
                builder.Append(charSet[rndGenerator.Next(0, charSet.Length)]);
            }

            return builder.ToString();
        }
    }
}
