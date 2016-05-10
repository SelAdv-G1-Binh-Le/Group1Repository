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
        static readonly By _mnGlobalSetting = By.XPath("//li[@class='mn-setting']");
        static readonly By _txtPageName = By.XPath("//input[@id='name']");
        static readonly By _btnPopupOk = By.XPath("//input[@id='OK']");
        static readonly By _mnMainBar = By.XPath("//div[@id='main-menu']");

        #endregion

        #region Elements
        public IWebElement MnMainBar
        {
            get { return Constant.WebDriver.FindElement(_mnMainBar); }
        }
        public IWebElement TxtPageName
        {
            get { return Constant.WebDriver.FindElement(_txtPageName); }
        }
        public IWebElement BtnPopupOk
        {
            get { return Constant.WebDriver.FindElement(_btnPopupOk); }
        }
        public IWebElement MnGlobalSetting
        {
            get { return Constant.WebDriver.FindElement(_mnGlobalSetting); }
        }
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

        public void ClickDropdownMenu(IWebElement parentControl, string childMenu)
        {
            parentControl.Click();
            IWebElement ChildLink = Constant.WebDriver.FindElement(By.LinkText(childMenu));
            ChildLink.Click();
        }


        #endregion

    }
}
