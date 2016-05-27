using OpenQA.Selenium;
using Group1Project.Common;
using OpenQA.Selenium.Support.UI;

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


        #endregion

        #region Elements

        public IWebElement BtnOK
        {
            get { return webDriver.FindElement(_btnOK); }
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
        /// Updated Date: 05/27/2016

        public void AddChartPanel(string displayname, string series)
        {
            TxtDisplayName.SendKeys(displayname);
            SelectElement SelectedCbo = new SelectElement(CbbSeriesField);
            SelectedCbo.SelectByValue(series);
            BtnOK.Click();

            CommonMethods.WaitForControl(webDriver, By.XPath("//select[@id='cbbPages']"), 5);
            
            
            
            BtnOK.Click();

            //CommonMethods.WaitForControl(webDriver, By.XPath(CommonMethods.XPathContainGenerate("a", pagename)), 10);
        }


        #endregion


    }
}
