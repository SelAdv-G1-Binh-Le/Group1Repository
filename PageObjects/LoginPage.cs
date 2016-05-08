using System;
using OpenQA.Selenium;
using Group1Project.Common;

namespace Group1Project.PageObjects
{
    class LoginPage : GeneralPage
    {
        #region Locators

        static readonly By _cboRepository = By.XPath("//select[@id='repository']");
        static readonly By _txtUsername = By.XPath("//input[@id='username']");
        static readonly By _txtPassword = By.XPath("//input[@id='password']");
        static readonly By _btnLogin = By.XPath("//div[@class='btn-login']");

               
        #endregion

        #region Elements
        public IWebElement CboRepository
        {
            get { return Constant.WebDriver.FindElement(_cboRepository); }
        }

        public IWebElement TxtUsername
        {
            get { return Constant.WebDriver.FindElement(_txtUsername); }
        }

        public IWebElement TxtPassword
        {
            get { return Constant.WebDriver.FindElement(_txtPassword); }
        }

        public IWebElement BtnLogin
        {
            get { return Constant.WebDriver.FindElement(_btnLogin); }
        }

        #endregion

        #region Methods


        #endregion
    }
}
