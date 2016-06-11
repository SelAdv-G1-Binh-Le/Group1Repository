using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Group1Project.Common;
using Group1Project.DataObjects;
using Group1Project.TestCases;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace Group1Project.PageObjects
{
    class AddDataProfilePage : GeneralPage
    {
        private IWebDriver driver;

        public AddDataProfilePage(IWebDriver webDriver)
            : base(webDriver)
        {
            this.driver = webDriver;
        }

        #region Locators;
        static readonly By _txtName = By.XPath("//input[@id='txtProfileName']");
        static readonly By _cmbItemType = By.XPath("//select[@id='cbbEntityType']");
        static readonly By _cmbRelatedData = By.XPath("//select[@id='cbbSubReport']");
        static readonly By _btnNext = By.XPath("//input[@type='button' and @value='Next']");
        static readonly By _btnFinish = By.XPath("//input[@type='button' and @value='Finish']");
        static readonly By _btnCancel = By.XPath("//input[@type='button' and @value='Cancel']");

        public IWebElement TxtName
        {
            get { return FindElement(_txtName, Constant.DefaultTimeout); }
        }
        public IWebElement CmbItemType
        {
            get { return FindElement(_cmbItemType, Constant.DefaultTimeout); }
        }
        public IWebElement CmbRelatedData
        {
            get { return FindElement(_cmbRelatedData, Constant.DefaultTimeout); }
        }
        public IWebElement BtnNext
        {
            get { return FindElement(_btnNext, Constant.DefaultTimeout); }
        }
        public IWebElement BtnFinish
        {
            get { return FindElement(_btnFinish, Constant.DefaultTimeout); }
        }
        public IWebElement BtnCancel
        {
            get { return FindElement(_btnCancel, Constant.DefaultTimeout); }
        }


        #endregion

    }
}
