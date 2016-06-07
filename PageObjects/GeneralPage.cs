using System;
using OpenQA.Selenium;
using Group1Project.Common;
using System.Diagnostics;
using OpenQA.Selenium.Support.UI;

namespace Group1Project.PageObjects
{
    public class GeneralPage
    {

        protected IWebDriver webDriver;

        #region Locators



        #endregion

        #region Elements




        #endregion

        #region Methods

        public GeneralPage(IWebDriver _webDriver)
        {
            this.webDriver = _webDriver;
        }

        public GeneralPage()
        {
        }

        /// <summary>
        /// Finds the element.
        /// </summary>
        /// <param name="by">The by.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/2/2016 - 04:07</datetime>
        public IWebElement FindElement(By by, long timeout = Constant.DefaultTimeout)
        {
            IWebElement webElement = null;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine("Finding element <{0}> ...",by.ToString());
            try
            {
                webElement = webDriver.FindElement(by);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0} in {1} milliseconds", ex.Message, stopWatch.ElapsedMilliseconds);
                if (ex is StaleElementReferenceException || ex is NullReferenceException)
                {
                    webElement = this.FindElement(by, ((timeout * 1000 - stopWatch.ElapsedMilliseconds) / 1000));
                    Console.WriteLine("Dang bi " + ex + "time eslapsed la: " + ((timeout * 1000 - stopWatch.ElapsedMilliseconds) / 1000));
                }
            }

            stopWatch.Stop();

            if (webElement == null)

                Console.WriteLine("Element <{0}> is NOT found in {1} milliseconds!", by.ToString(), stopWatch.ElapsedMilliseconds);
            else
            {
                Console.WriteLine("Element <{0}> is found in {1} milliseconds!", by.ToString(), stopWatch.ElapsedMilliseconds);
            }

            return webElement;
        }

        #endregion
    }
}
