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
        static By _txtCategoryCaption = By.XPath("//input[@id='txtCategoryXAxis']");
        static By _txtSeriesCaption = By.XPath("//input[@id='txtValueYAxis']");
        static By _cbbCategoryField = By.XPath("//select[@id='cbbCategoryField']");
        static By _chkShowTitle = By.XPath("//input[@id='chkShowTitle']");
        static By _radChartStyle2D = By.XPath("//input[@id='rdoChartStyle2D']");
        static By _radChartStyle3D = By.XPath("//input[@id='rdoChartStyle3D']");
        static By _radLegendsNone = By.XPath("//input[@id='radPlacementNone']");
        static By _radLegendsTop = By.XPath("//input[@id='radPlacementTop']");
        static By _radLegendsRight = By.XPath("//input[@id='radPlacementRight']");
        static By _radLegendsBottom = By.XPath("//input[@id='radPlacementBottom']");
        static By _radLegendsLeft = By.XPath("//input[@id='radPlacementLeft']");
        static By _chkDataLabelsSeries = By.XPath("//input[@id='chkSeriesName']");
        static By _chkDataLabelsCategories = By.XPath("//input[@id='chkCategoriesName']");
        static By _chkDataLabelsValue = By.XPath("//input[@id='chkValue']");
        static By _chkDataLabelsPercentage = By.XPath("//input[@id='chkPercentage']");

        #endregion

        #region Elements

        public IWebElement ChkDataLabelsPercentage
        {
            get { return FindElement(_chkDataLabelsPercentage, Constant.DefaultTimeout); }
        }
        public IWebElement ChkDataLabelsValue
        {
            get { return FindElement(_chkDataLabelsValue, Constant.DefaultTimeout); }
        }
        public IWebElement ChkDataLabelsCategories
        {
            get { return FindElement(_chkDataLabelsCategories, Constant.DefaultTimeout); }
        }
        public IWebElement ChkDataLabelsSeries
        {
            get { return FindElement(_chkDataLabelsSeries, Constant.DefaultTimeout); }
        }
        public IWebElement RadLegendsLeft
        {
            get { return FindElement(_radLegendsLeft, Constant.DefaultTimeout); }
        }
        public IWebElement RadLegendsBottom
        {
            get { return FindElement(_radLegendsBottom, Constant.DefaultTimeout); }
        }
        public IWebElement RadLegendsRight
        {
            get { return FindElement(_radLegendsRight, Constant.DefaultTimeout); }
        }

        public IWebElement RadLegendsTop
        {
            get { return FindElement(_radLegendsTop, Constant.DefaultTimeout); }
        }
        public IWebElement RadLegendsNone
        {
            get { return FindElement(_radLegendsNone, Constant.DefaultTimeout); }
        }
        public IWebElement RadChartStyle3D
        {
            get { return FindElement(_radChartStyle3D, Constant.DefaultTimeout); }
        }
        public IWebElement RadChartStyle2D
        {
            get { return FindElement(_radChartStyle2D, Constant.DefaultTimeout); }
        }

        public IWebElement ChkShowTitle
        {
            get { return FindElement(_chkShowTitle, Constant.DefaultTimeout); }
        }
      
        public IWebElement CbbCategoryField
        {
            get { return FindElement(_cbbCategoryField, Constant.DefaultTimeout); }
        }

        public IWebElement TxtSeriesCaption
        {
            get { return FindElement(_txtSeriesCaption, Constant.DefaultTimeout); }
        }

        public IWebElement TxtCategoryCaption
        {
            get { return FindElement(_txtCategoryCaption, Constant.DefaultTimeout); }
        }

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
        public PanelsPage AddChartPanelSuccess
            (string displayname = Constant.DefaultDisplayName,
            string title = "",
            string type = Constant.DefaultChartType,
            string categoryvalue = "name",
            string series = Constant.DefaultSeriesValue,
            string dataprofiletext = "Action Implementation By Status",
            bool showtitle = false,
            string style = "2D",
            string legends = "Bottom",
            bool dataLabelsSeries = false,
            bool dataLabelsCategories = false,
            bool dataLabelsValue = false,
            bool dataLabelsPercentage = false)
        {
            Console.WriteLine("- Add Chart Panel Success");

            CommonMethods.WaitForControl(webDriver, _cbbProfile, Constant.DefaultTimeout);            
            this.CbbProfile.SelectByText(dataprofiletext);

            switch (legends)
            {
                case "None":
                    this.RadLegendsNone.Click();
                    break;
                case "Top":
                    this.RadLegendsTop.Click();
                    break;
                case "Right":
                    this.RadLegendsRight.Click();
                    break;
                case "Bottom":
                    this.RadLegendsBottom.Click();
                    break;
                case "Left":
                    this.RadLegendsLeft.Click();
                    break;
            }

            this.TxtChartTitle.Set(title);

            this.CbbChartType.SelectByValue(type);

            this.ChkDataLabelsSeries.Check(dataLabelsSeries);
            this.ChkDataLabelsCategories.Check(dataLabelsCategories);
            this.ChkDataLabelsValue.Check(dataLabelsValue);
            this.ChkDataLabelsPercentage.Check(dataLabelsPercentage);

            this.TxtDisplayName.Set(displayname);
            this.CbbSeriesField.SelectByValue(series);


            if (style == "3D") this.RadChartStyle3D.Click();
            if (style == "2D") this.RadChartStyle2D.Click();
            this.ChkShowTitle.Check(showtitle);

            if (CbbCategoryField.Enabled) this.CbbCategoryField.SelectByValue(categoryvalue);
            //Click OK button
            this.BtnOK.Click();          

            //CommonMethods.WaitForControlDisappear(webDriver, By.XPath("//div[@class='ui-dialog-overlay custom-overlay']"), Constant.DefaultTimeout);
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
        public string AddChartPanelUnsuccess
            (string displayname = "",
            string title = "",
            string type = Constant.DefaultChartType,
            string categoryvalue = "name",
            string series = Constant.DefaultSeriesValue,
            string dataprofiletext = "Action Implementation By Status",
            bool showtitle = false,
            string style = "2D",
            string legends = "Bottom",
            bool dataLabelsSeries = false,
            bool dataLabelsCategories = false,
            bool dataLabelsValue = false,
            bool dataLabelsPercentage = false)
        {
            Console.WriteLine("- Add Chart Panel Unsuccess");
            CommonMethods.WaitForControl(webDriver, _cbbProfile, Constant.DefaultTimeout);
            this.CbbProfile.SelectByText(dataprofiletext);

            CommonMethods.WaitForControl(webDriver, _txtDisplayName, Constant.DefaultTimeout);

            switch (legends)
            {
                case "None":
                    this.RadLegendsNone.Click();
                    break;
                case "Top":
                    this.RadLegendsTop.Click();
                    break;
                case "Right":
                    this.RadLegendsRight.Click();
                    break;
                case "Bottom":
                    this.RadLegendsBottom.Click();
                    break;
                case "Left":
                    this.RadLegendsLeft.Click();
                    break;
            }

            this.TxtChartTitle.Set(title);

            this.CbbChartType.SelectByValue(type);

            this.ChkDataLabelsSeries.Check(dataLabelsSeries);
            this.ChkDataLabelsCategories.Check(dataLabelsCategories);
            this.ChkDataLabelsValue.Check(dataLabelsValue);
            this.ChkDataLabelsPercentage.Check(dataLabelsPercentage);

            this.TxtDisplayName.Set(displayname);
            this.CbbSeriesField.SelectByValue(series);


            if (style == "3D") this.RadChartStyle3D.Click();
            if (style == "2D") this.RadChartStyle2D.Click();
            this.ChkShowTitle.Check(showtitle);

            if (CbbCategoryField.Enabled) this.CbbCategoryField.SelectByValue(categoryvalue);

            //Click OK button
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
            LnkCloseButton.Click();
            CommonMethods.WaitForControlDisappear(webDriver, _dlgOverlay, Constant.DefaultTimeout);
            return new PanelsPage(webDriver);
        }

        #endregion

    }
}
