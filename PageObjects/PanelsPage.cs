using System;
using OpenQA.Selenium;
using Group1Project.Common;
using OpenQA.Selenium.Support.UI;

namespace Group1Project.PageObjects
{
    /// <summary>
    /// 
    /// </summary>
    /// <author>Diep Duong</author>
    /// <datetime>6/8/2016 - 19:47</datetime>
    /// <seealso cref="Group1Project.PageObjects.GeneralPage" />
    class PanelsPage : GeneralPage
    {

        private IWebDriver driver;

        #region Locators

        static readonly By _lnkAddNew = By.XPath("//a[(text()='Add New')]");


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
        public PanelsPage DeletePanel(string panelname)
        {
            By dynamicXpath = By.XPath("//td[contains(.,'" + panelname + "')]//following::a[contains(.,'Delete')][1]");
            MainPage mainpage = new MainPage(webDriver);
            mainpage.GotoPanelsPage().FindElement(dynamicXpath, Constant.DefaultTimeout).Click();
            webDriver.SwitchTo().Alert().Accept();
            CommonMethods.WaitForControlDisappear(webDriver, By.XPath("//table[@class='GridView']//a[text()='" + panelname + "']"), Constant.DefaultTimeout);
            return this;
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
            IWebElement webElement = this.FindElement(By.XPath("//table[@class='GridView']//a[text()='" + panelname + "']"), Constant.DefaultTimeout);
            if (webElement == null) return false; else return true;
        }

        /// <summary>
        /// Clicks the panel.
        /// </summary>
        /// <param name="panelname">The panelname.</param>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/6/2016 - 00:58</datetime>
        public PanelsPage ClickPanel(string panelname)
        {
            this.FindElement(By.XPath("//table[@class='GridView']//a[text()='" + panelname + "']"), Constant.DefaultTimeout).Click();
            return this;
        }

        /// <summary>
        /// Deletes all panels.
        /// </summary>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/8/2016 - 19:37</datetime>
        public PanelsPage DeleteAllPanels()
        {
            MainPage mainPage = new MainPage(webDriver);
            mainPage.GotoPanelsPage();
            if (CommonMethods.IsElementPresent(webDriver, By.XPath("//a[text()='Check All']")))
            {
                FindElement(By.XPath("//a[text()='Check All']")).Click();
                FindElement(By.XPath("//a[@href='javascript:Dashboard.deletePanels();']")).Click();
                CommonMethods.CloseAlertAndGetItsText(webDriver);
                CommonMethods.WaitForControlDisappear(webDriver, By.XPath("//a[@href='javascript:Dashboard.deletePanels();']"), Constant.DefaultTimeout);
            }
            return this;
        }


        #endregion


    }
}
