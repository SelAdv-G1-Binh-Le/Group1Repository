using System;
using OpenQA.Selenium;
using Group1Project.Common;
using OpenQA.Selenium.Support.UI;

namespace Group1Project.PageObjects
{
    class PanelsPage : GeneralPage
    {

        private IWebDriver driver;

        #region Locators

        static readonly By _lnkAddNew = By.XPath("//a[contains(@href,'AddPanel')]");


        #endregion

        #region Elements
        public IWebElement LnkAddNew
        {
            get { return FindElement(_lnkAddNew, Constant.DefaultTimeout); }
        }


        #endregion

        #region Methods


        public PanelsPage(IWebDriver webDriver)
            : base(webDriver)
        {
            this.driver = webDriver;
        }

        /// <summary>
        /// Deletes the panel.
        /// </summary>
        /// <param name="panelname">The panelname.</param>
        /// <author>Diep Duong</author>
        /// <datetime>6/2/2016 - 05:13</datetime>
        public void DeletePanel(string panelname)
        {
            MainPage mainpage = new MainPage(webDriver);
            mainpage.GotoPanelsPage();
            By dynamicXpath = By.XPath("//td[contains(.,'" + panelname + "')]//following::a[contains(.,'Delete')]");
            FindElement(dynamicXpath, Constant.DefaultTimeout).Click();
            webDriver.SwitchTo().Alert().Accept();
        }

        /// <summary>
        /// Determines whether [is panel present] [the specified panelname].
        /// </summary>
        /// <param name="panelname">The panelname.</param>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/4/2016 - 16:36</datetime>
        public bool IsPanelPresent(string panelname)
        {
            IWebElement webElement = FindElement(By.XPath("//table[@class='GridView']//a[text()='" + panelname + "']"), Constant.DefaultTimeout);
            if (webElement == null) return false; else return true;
        }

        #endregion


    }
}
