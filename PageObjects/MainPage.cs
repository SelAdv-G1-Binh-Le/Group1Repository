using System;
using OpenQA.Selenium;
using Group1Project.Common;

namespace Group1Project.PageObjects
{
    class MainPage : GeneralPage
    {

        //string dynamicRepository = Constant.Repository2;

        #region Locators

        static readonly By _lblWelcome = By.XPath("//a[@href='#Welcome']");
        static readonly By _lblRepository = By.XPath("//a[@href='#Repository']");
        static readonly By _lnkLogout = By.XPath("//a[@href='logout.do']");

        #endregion

        #region Elements
        public IWebElement LblWelcome
        {
            get { return Constant.WebDriver.FindElement(_lblWelcome); }
        }

        public IWebElement LnkLogout
        {
            get { return Constant.WebDriver.FindElement(_lnkLogout); }
        }


        public IWebElement LblRepository
        {
            get { return Constant.WebDriver.FindElement(_lblRepository); }
        }


        #endregion

        #region Methods

        public LoginPage Logout()
        {
            this.LblWelcome.Click();
            this.LnkLogout.Click();

            return new LoginPage();
        }


        public MainPage ChangeRepository(string repository)
        {
            this.LblRepository.Click();
            IWebElement DynamiclblRepository = Constant.WebDriver.FindElement(By.XPath("//ul[@id='ulListRepositories']//a[contains(.,'" + repository + "')]"));
            DynamiclblRepository.Click();
            return this;
        }




        #endregion

    }
}
