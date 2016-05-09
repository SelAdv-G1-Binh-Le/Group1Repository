using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group1Project.PageObjects
{
    class MainPage : GeneralPage
    {


        #region Locators

        static readonly By _lblWelcome = By.XPath("//a[@href='#Welcome']");
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
        #endregion

        #region Methods

        public LoginPage Logout()
        {
            this.LblWelcome.Click();
            this.LnkLogout.Click();

            return new LoginPage();
        }


        #endregion
    }
}
