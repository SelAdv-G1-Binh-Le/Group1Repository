using System;
using OpenQA.Selenium;
using Group1Project.Common;
using OpenQA.Selenium.Support.UI;

namespace Group1Project.PageObjects
{
    class PanelsPage : GeneralPage
    {

         public IWebDriver webDriver;

        #region Locators

         static readonly By _lnkAddNew = By.XPath("//a[contains(@href,'AddPanel')]");
       

        #endregion

        #region Elements
         public IWebElement LnkAddNew
        {
            get { return webDriver.FindElement(_lnkAddNew); }
        }

      

        #endregion

        #region Methods


        public PanelsPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }



        #endregion






    }
}
