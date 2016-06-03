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

                #endregion
        #region Elements
        public IWebElement BtnOKPanelConfiguration
        {
            get { return FindElement(_btnOKPanelConfiguration, Constant.DefaultTimeout); }
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
        /// Adds the chart panel.
        /// </summary>
        /// <param name="displayname">The displayname.</param>
        /// <param name="series">The series.</param>
        /// Author: Diep Duong
        /// Updated Date: 05/30/2016

        public MainPage AddChartPanelSuccess(string displayname, string series)
        {
            Console.WriteLine("- AddChartPanelSuccess");       
            SelectElement SelectedCbo = new SelectElement(CbbSeriesField);
            SelectedCbo.SelectByValue(series);
            TxtDisplayName.SendKeys(displayname);
            BtnOK.Click();

            if (CommonMethods.IsElementPresent(webDriver,_btnOKPanelConfiguration))
            {
                BtnOKPanelConfiguration.Click();                
            }

            CommonMethods.WaitForControlDisappear(webDriver, By.XPath("//div[@class='ui-dialog-overlay custom-overlay']"), 10);
            return new MainPage(webDriver);
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
