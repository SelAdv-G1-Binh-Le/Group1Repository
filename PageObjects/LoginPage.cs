using System;
using OpenQA.Selenium;
using Group1Project.Common;
using OpenQA.Selenium.Support.UI;

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
        public LoginPage Open()
        {
            Constant.WebDriver.Navigate().GoToUrl(Constant.LoginPageURL);
            return this;
        }

        public MainPage Login(string username, string password, string repository = Constant.DefaultRepository)
        {
            Console.WriteLine("Select Repository '{0}'", repository);

            SelectElement CboRepository = new SelectElement(this.CboRepository);
            CboRepository.SelectByText(repository);

            Console.WriteLine("Enter username '{0}'", username);
            TxtUsername.SendKeys(username);

            Console.WriteLine("Enter password '{0}'", password);
            TxtPassword.SendKeys(password);

            if (password == "")
            {
                TxtPassword.Clear();
            }

            Console.WriteLine("Click Login button");
            BtnLogin.Click();

            //Land on Home Page
            return new MainPage();
        }

        #endregion
    }
}
