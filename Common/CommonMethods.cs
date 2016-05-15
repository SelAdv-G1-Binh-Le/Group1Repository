using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;


namespace Group1Project.Common
{
    public static class CommonMethods
    {
        public static IWebDriver WebDriver;
        public static bool acceptNextAlert = true;

              public static string CreateRandomString(int length)
        {
            Thread.Sleep(1000);
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string CreateRandomEmail(string emaildomain)
        {
            return CreateRandomString(15) + "@" + emaildomain;
        }

        public static bool IsElementPresent(By by)
        {
            try
            {
                IWebElementExtension.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static void WaitForControl(By by, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(TestCases.Testbase.WebDriver, TimeSpan.FromSeconds(timeout));
            wait.Until(d => IWebElementExtension.FindElement(by));
        }        
        public static bool WaitForControlEnable(By by, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(TestCases.Testbase.WebDriver, TimeSpan.FromSeconds(timeout));
            return wait.Until(d => IWebElementExtension.FindElement(by).Enabled);
        }

        public static string ConvertDateTimeToString(DateTime dt)
        {
            return dt.Month.ToString() + '/' + dt.Day.ToString() + '/' + dt.Year.ToString();
        }

        public static DateTime ConvertStringToDateTime(string str)
        {
            string[] words = str.Split('/');
            DateTime dt = new DateTime(int.Parse(words[2]), int.Parse(words[0]), int.Parse(words[1]));
            return dt;
        }


        public static string GetComboboxSelectedValue(IWebElement combobox)
        {
            SelectElement SelectedCbo = new SelectElement(combobox);
            return SelectedCbo.SelectedOption.Text;
        }

        public static bool IsAlertPresent(IWebDriver WebDriver)
        {
            try
            {
                WebDriver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        public static string CloseAlertAndGetItsText(IWebDriver WebDriver)
        {
            try
            {
                IAlert alert = WebDriver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        public static string RandomString()
        {
            string Random = DateTime.Now.ToString("ddMMMyyHHmmssfff");
            return Random;
        }
           

    }
}
