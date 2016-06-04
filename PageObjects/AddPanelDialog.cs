using OpenQA.Selenium;
using Group1Project.Common;
using OpenQA.Selenium.Support.UI;
using System;

namespace Group1Project.PageObjects
{
    class AddPanelDialog : GeneralPage
    {

        private IWebDriver driver;

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
        static By _lnkCloseButton = By.XPath("//a[@class='ui-dialog-titlebar-close']");
        static By _txtChartTitle = By.XPath("//input[@id='txtChartTitle']");
        static By _cbbChartType = By.XPath("//select[@id='cbbChartType']");



        #endregion

        #region Elements

        public IWebElement CbbChartType
        {
            get { return FindElement(_cbbChartType, Constant.DefaultTimeout); }
        }
        public IWebElement TxtChartTitle
        {
            get { return FindElement(_txtChartTitle, Constant.DefaultTimeout); }
        }

        public IWebElement DlgOverlay
        {
            get { return FindElement(_dlgOverlay, Constant.DefaultTimeout); }
        }

        public IWebElement BtnOKPanelConfiguration
        {
            get { return FindElement(_btnOKPanelConfiguration, Constant.DefaultTimeout); }
        }

        public IWebElement LnkCloseButton
        {
            get { return FindElement(_lnkCloseButton, Constant.DefaultTimeout); }
        }

        public IWebElement CbbProfile
        {
            get { return FindElement(_cbbProfile, Constant.DefaultTimeout); }
        }

        public IWebElement LgdIndicatorSettings
        {
            get { return FindElement(_lgdIndicatorSettings, Constant.DefaultTimeout); }
        }

        public IWebElement LgdChartSettings
        {
            get { return FindElement(_lgdChartSettings, Constant.DefaultTimeout); }
        }

        public IWebElement RadReportType
        {
            get { return FindElement(_radReportType, Constant.DefaultTimeout); }
        }

        public IWebElement RadIndicatorType
        {
            get { return FindElement(_radIndicatorType, Constant.DefaultTimeout); }
        }

        public IWebElement BtnOK
        {
            get { return FindElement(_btnOK, Constant.DefaultTimeout); }
        }

        public IWebElement RadTypeChart
        {
            get { return FindElement(_radTypeChart, Constant.DefaultTimeout); }
        }

        public IWebElement TxtDisplayName
        {
            get { return FindElement(_txtDisplayName, Constant.DefaultTimeout); }
        }

        public IWebElement CbbSeriesField
        {
            get { return FindElement(_cbbSeriesField, Constant.DefaultTimeout); }
        }

        #endregion

        #region Methods


        public AddPanelDialog(IWebDriver webDriver)
            : base(webDriver)
        {
            this.driver = webDriver;
        }

        /// <summary>
        /// Adds the chart panel success.
        /// </summary>
        /// <param name="displayname">The displayname.</param>
        /// <param name="series">The series.</param>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/3/2016 - 09:40</datetime>
        public PanelsPage AddChartPanelSuccess(string displayname, string title = "", string type = Constant.DefaultChartType, string series = Constant.DefaultSeriesValue)
        {
            Console.WriteLine("- Add Chart Panel Success");
            CbbSeriesField.SelectByValue(series);
            TxtDisplayName.Set(displayname);
            TxtChartTitle.Set(title);
            CbbChartType.SelectByValue(type);
            BtnOK.Click();

            //Handle for creating new Panel at MainPage
            if (CommonMethods.IsElementPresent(webDriver, _btnOKPanelConfiguration))
            {
                BtnOKPanelConfiguration.Click();
            }

            CommonMethods.WaitForControlDisappear(webDriver, By.XPath("//div[@class='ui-dialog-overlay custom-overlay']"), Constant.DefaultTimeout);
            return new PanelsPage(webDriver);
        }

        /// <summary>
        /// Adds the chart panel unsuccess.
        /// </summary>
        /// <param name="displayname">The displayname.</param>
        /// <param name="title">The title.</param>
        /// <param name="type">The type.</param>
        /// <param name="series">The series.</param>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/4/2016 - 16:31</datetime>
        public string AddChartPanelUnsuccess(string displayname = "", string title = "", string type = Constant.DefaultChartType, string series = Constant.DefaultSeriesValue)
        {
            Console.WriteLine("- Add Chart Panel Unsuccess");
            CbbSeriesField.SelectByValue(series);
            TxtDisplayName.Set(displayname);
            TxtChartTitle.Set(title);
            CbbChartType.SelectByValue(type);
            BtnOK.Click();
            string alert = CommonMethods.CloseAlertAndGetItsText(webDriver);
            this.Close();
            return alert;
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

        /// <summary>
        /// Closes this instance.
        /// </summary>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/4/2016 - 16:31</datetime>
        public PanelsPage Close()
        {
            CommonMethods.WaitForControlDisplayed(webDriver,_lnkCloseButton, Constant.DefaultTimeout);
            LnkCloseButton.Click();
            CommonMethods.WaitForControlDisappear(webDriver, _dlgOverlay, Constant.DefaultTimeout);
            return new PanelsPage(webDriver);
        }
        #endregion


    }
}
