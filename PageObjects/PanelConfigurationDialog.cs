using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Group1Project.Common;
using OpenQA.Selenium.Support.UI;

namespace Group1Project.PageObjects
{
    /// <summary>
    /// 
    /// </summary>
    /// <author>Diep Duong</author>
    /// <datetime>6/6/2016 - 23:06</datetime>
    /// <seealso cref="Group1Project.PageObjects.GeneralPage" />
    class PanelConfigurationDialog : GeneralPage
    {
        private IWebDriver driver;
                
        #region Locators

        static By _cbbPages = By.XPath("//select[@id='cbbPages']");
        static By _txtHeight = By.XPath("//input[@id='txtHeight']");
        static By _txtFolder = By.XPath("//input[@id='txtFolder']");
        static By _btnOK = By.XPath("//div[@id='div_panelConfigurationDlg']//input[@id='OK']");
        static By _btnCancel = By.XPath("//div[@id='div_panelConfigurationDlg']//input[@id='Cancel']");
        
        #endregion

        #region Elements
        public IWebElement BtnCancel
        {
            get { return FindElement(_btnCancel, Constant.DefaultTimeout); }
        }
        public IWebElement BtnOK
        {
            get { return FindElement(_btnOK, Constant.DefaultTimeout); }
        }
        public IWebElement TxtFolder
        {
            get { return FindElement(_txtFolder, Constant.DefaultTimeout); }
        }
        public IWebElement TxtHeight
        {
            get { return FindElement(_txtHeight, Constant.DefaultTimeout); }
        }
        public IWebElement CbbPages
        {
            get { return FindElement(_cbbPages, Constant.DefaultTimeout); }
        }
        

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelConfigurationDialog"/> class.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <author>Diep Duong</author>
        /// <datetime>6/6/2016 - 23:26</datetime>
        public PanelConfigurationDialog(IWebDriver webDriver)
            : base(webDriver)
        {
            this.driver = webDriver;
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/6/2016 - 23:26</datetime>
        public MainPage Close()
        {
            this.BtnCancel.Click();
            CommonMethods.WaitForControlDisappear(webDriver, By.XPath("//div[@class='ui-dialog-overlay custom-overlay'][contains(@style,'top: 0px')]"), Constant.DefaultTimeout);
            return new MainPage(webDriver);
        }
        /// <summary>
        /// Edits the panel unsuccess.
        /// </summary>
        /// <param name="pagename">The pagename.</param>
        /// <param name="height">The height.</param>
        /// <param name="folder">The folder.</param>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/6/2016 - 23:26</datetime>
        public string EditPanelUnsuccess(string pagename="Execution Dashboard", string height="", string folder="")
        {
            Console.WriteLine("- Edit Panel Unsuccess:");
            CommonMethods.WaitForControl(webDriver, By.XPath("//div[@class='ui-dialog-overlay custom-overlay'][contains(@style,'top: 0px')]"), Constant.DefaultTimeout);
            this.CbbPages.SelectByText(pagename);
            this.TxtHeight.Set(height);
            this.TxtFolder.Set(folder);            
            this.BtnOK.Click();
            string alert = CommonMethods.CloseAlertAndGetItsText(webDriver);
            this.Close();
            return alert;
        }



        #endregion




    }
}
