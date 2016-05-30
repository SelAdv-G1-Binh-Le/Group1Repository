using OpenQA.Selenium;
using Group1Project.Common;
using OpenQA.Selenium.Support.UI;
using System;

namespace Group1Project.PageObjects
{
    class AddPanelDialog : GeneralPage
    {

        public IWebDriver webDriver;

        #region Locators

        static By _radTypeChart = By.XPath("//input[@id='radPanelType0']");
        static By _txtDisplayName = By.XPath("//input[@id='txtDisplayName']");
        static By _cbbSeriesField = By.XPath("//select[@id='cbbSeriesField']");
        static By _btnOK = By.XPath("//input[@id='OK']");
        static By _btnOKPanelConfiguration = By.XPath("//div[@id='div_panelConfigurationDlg']//input[@id='OK']");
        static By _radIndicatorType = By.XPath("//input[@id='radPanelType1']");
        static By _radReportType = By.XPath("//input[@id='radPanelType2']");
        static By _lgdChartSettings = By.XPath("//legend[contains(.,'Chart Settings')]");
        static By _lgdIndicatorSettings = By.XPath("//legend[contains(.,'Indicator Settings')]");
        static By _dlgOverlay = By.XPath("//div[@class='ui-dialog-overlay custom-overlay']");
        static By _cbbProfile = By.XPath("//select[@id='cbbProfile']");


        //input[@id='radPanelType1']

        #endregion

        public IWebElement CbbProfile
        {
            get { return webDriver.FindElement(_cbbProfile); }
        }

        public IWebElement LgdIndicatorSettings
        {
            get { return webDriver.FindElement(_lgdIndicatorSettings); }
        }

        public IWebElement LgdChartSettings
        {
            get { return webDriver.FindElement(_lgdChartSettings); }
        }

        #region Elements

        public IWebElement RadReportType
        {
            get { return webDriver.FindElement(_radReportType); }
        }

        public IWebElement RadIndicatorType
        {
            get { return webDriver.FindElement(_radIndicatorType); }
        }

        public IWebElement BtnOK
        {
            get { return webDriver.FindElement(_btnOK); }
        }

        public IWebElement BtnOKPanelConfiguration
        {
            get { return webDriver.FindElement(_btnOKPanelConfiguration); }
        }

        public IWebElement RadTypeChart
        {
            get { return webDriver.FindElement(_radTypeChart); }
        }

        public IWebElement TxtDisplayName
        {
            get { return webDriver.FindElement(_txtDisplayName); }
        }

        public IWebElement CbbSeriesField
        {
            get { return webDriver.FindElement(_cbbSeriesField); }
        }

        #endregion

        #region Methods

        public AddPanelDialog(IWebDriver webDriver)
        {
            this.webDriver = webDriver;

        }


        /// <summary>
        /// Adds the chart panel.
        /// </summary>
        /// <param name="displayname">The displayname.</param>
        /// <param name="series">The series.</param>
        /// Author: Diep Duong
        /// Updated Date: 05/30/2016

        public void AddChartPanel(string displayname, string series)
        {
            TxtDisplayName.SendKeys(displayname);
            SelectElement SelectedCbo = new SelectElement(CbbSeriesField);
            SelectedCbo.SelectByValue(series);
            BtnOK.Click();
            CommonMethods.WaitForControlDisappear(webDriver, By.XPath("//div[@class='ui-dialog-overlay custom-overlay']"), 10);
        }

        /// <summary>
        /// Adds the report panel.
        /// </summary>
        /// <param name="displayname">The displayname.</param>
        /// Author: Diep Duong
        /// Updated Date: 05/30/2016
        public void AddReportPanel(string displayname)
        {
            Console.WriteLine("Click Report radio button");
            this.RadReportType.Click();
            CommonMethods.WaitForControlDisappear(webDriver, _lgdChartSettings, 10);
            Console.WriteLine("Enter Display name");
            TxtDisplayName.SendKeys(displayname);
            BtnOK.Click();
            CommonMethods.WaitForControlDisappear(webDriver, By.XPath("//div[@class='ui-dialog-overlay custom-overlay']"), 10);

        }


        #endregion


    }
}
