using System;
using OpenQA.Selenium;
using Group1Project.Common;
using System.Diagnostics;

namespace Group1Project.PageObjects
{
    public class GeneralPage
    {

        public IWebDriver webDriver;

        #region Locators



        #endregion

        #region Elements




        #endregion

        #region Methods

        public GeneralPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public GeneralPage()
        {
        }

        public IWebElement FindElement(By by, long timeout)
        {
            Console.WriteLine("FindElement: {0}", by.ToString());
            IWebElement webElement = null;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                
                webElement = webDriver.FindElement(by);               
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0} in {1} seconds", ex.ToString(), stopWatch.ElapsedMilliseconds / 1000);
                if (ex is StaleElementReferenceException || ex is NullReferenceException)
                {
                    webElement = this.FindElement(by, ((timeout * 1000 - stopWatch.ElapsedMilliseconds) / 1000));
                }
            }

            stopWatch.Stop();
            return webElement;
        }



        #endregion
    }
}
