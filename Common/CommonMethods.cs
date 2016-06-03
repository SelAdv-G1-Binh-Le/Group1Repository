using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using Group1Project.TestCases;
using OpenQA.Selenium.Support;
using System.Diagnostics;
using Group1Project.PageObjects;


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

        public static bool IsElementPresent(IWebDriver webDriver, By by)
        {
            try
            {
                webDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        /// <summary>
        /// Clicks the specified web driver.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="by">The by.</param>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/3/2016 - 02:19</datetime>
        public static bool Click(IWebElement iwebelement)
        {                      
            try
            {
                iwebelement.Click();
            }
            catch (WebDriverException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Waits for control.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="by">The by.</param>
        /// <param name="timeout">The timeout.</param>
        /// <author>Diep Duong</author>
        /// <datetime>6/2/2016 - 22:43</datetime>
        public static void WaitForControl(IWebDriver webDriver, By by, int timeout)
        {
            GeneralPage generalPage = new GeneralPage(webDriver);
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
            wait.Until(d => generalPage.FindElement(by,timeout));
        }


        /// <summary>
        /// Waits for control.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="webElement">The web element.</param>
        /// <param name="timeout">The timeout.</param>
        /// <author>Diep Duong</author>
        /// <datetime>6/3/2016 - 02:30</datetime>
        public static void WaitForControl(IWebDriver webDriver, IWebElement webElement, int timeout)
        {
            GeneralPage generalPage = new GeneralPage(webDriver);
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
            wait.Until(d => webElement.Enabled);
        }

        public static void WaitForControlDisappear(IWebDriver webDriver, By by, int timeout)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            bool flag = CommonMethods.IsElementPresent(webDriver, by);

            while (flag && timeout > 0)
            {
                Thread.Sleep(1000);
                flag = CommonMethods.IsElementPresent(webDriver, by);
                timeout = timeout - 1;
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("Wait For {0} disappears in {1} miliseconds! ", by.ToString(), ts.Milliseconds);

        }

        public static bool WaitForControlEnable(IWebDriver webDriver, By by, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
            return wait.Until(d => webDriver.FindElement(by).Enabled);
        }

        public static void WaitForControlEnable(IWebDriver webDriver, IWebElement webelement, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
            wait.Until(d => webelement.Enabled);
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

        /// <summary>
        /// Closes the alert and get its text.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/2/2016 - 22:33</datetime>
        public static string CloseAlertAndGetItsText(IWebDriver webDriver)
        {
            try
            {
                IAlert alert = webDriver.SwitchTo().Alert();
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

        public static void WaitAndClickControl(IWebDriver webDriver, string type, string property, string value, string selectvalue)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(string.Format("//{0}[contains({1},'{2}')]", type, property, value))));
            //if (type == "select")
            //{
            //    //SelectElement box = new SelectElement(Testbase.WebDriver.FindElement(By.XPath(string.Format("//{0}[{1}='{2}']", type, property, value))));
            //    //box.SelectByText(selectvalue);
            //    IWebElement box = Testbase.WebDriver.FindElement(By.XPath(string.Format("//{0}[contains({1},'{2}')]", type, property, value)));
            //    box.Click();
            //}
            //else
            //{
            //    Testbase.WebDriver.FindElement(By.XPath(string.Format("//{0}[contains({1},'{2}')]", type, property, value))).Click();
            //}
            webDriver.FindElement(By.XPath(string.Format("//{0}[contains({1},'{2}')]", type, property, value))).Click();
        }


        /// <summary>
        /// xes the path contain generate.
        /// </summary>
        /// <param name="tagname">The tagname.</param>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/2/2016 - 22:33</datetime>
        public static string XPathContainGenerate(string tagname, string str)
        {
            //Use it while the xPath has space characters 
            string xPath = "//" + tagname;
            string[] words = str.Split(' ');
            foreach (string word in words)
            {
                xPath = xPath + "[contains(.,'" + word + "')]";
            }
            return xPath;
        }
        public static void WaitUntilControlDisappear(IWebDriver webDriver, string tag, string property, string value)
        {
            bool check = CommonMethods.IsElementPresent(webDriver, By.XPath("//" + tag + "[" + property + "='" + value + "']"));
            if (check == true)
            {
                Thread.Sleep(1000);
            }
        }




    }
}
