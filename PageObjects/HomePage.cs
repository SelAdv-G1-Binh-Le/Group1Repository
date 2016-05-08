using System;
using OpenQA.Selenium;
using Group1Project.Common;

namespace Group1Project.PageObjects
{
    class HomePage : GeneralPage
    {

        #region Locators

        static readonly By _lblWelcome = By.XPath("//a[@href='#Welcome']");

        #endregion

        #region Elements
        public IWebElement LblWelcome
        {
            get { return Constant.WebDriver.FindElement(_lblWelcome); }
        }

        #endregion

    }
}
