using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;


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
                Constant.WebDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static void WaitForControl(By by, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(timeout));
            wait.Until(d => d.FindElement(by));
        }


        public static bool WaitForControlEnable(By by, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(timeout));
            return wait.Until(d => d.FindElement(by).Enabled);
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

        public static bool IsAlertPresent()
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

        public static string CloseAlertAndGetItsText()
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
        public static void Highlight(this  IWebElement context)
        {
            var rc = (RemoteWebElement)context;
            var driver = (IJavaScriptExecutor)rc.WrappedDriver;
            var script = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: red""; ";
            driver.ExecuteScript(script, rc);

            Thread.Sleep(3000);

            var clear = @"arguments[0].style.cssText = ""border-width: 0px; border-style: solid; border-color: red""; ";
            driver.ExecuteScript(clear, rc);

        }

    }
}
