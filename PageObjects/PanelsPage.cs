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
            get { return webDriver.FindElement(_lnkAddNew); }
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



        #endregion


    }
}
