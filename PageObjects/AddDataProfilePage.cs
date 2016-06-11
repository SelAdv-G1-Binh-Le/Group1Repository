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
        static readonly By _txtFilterList = By.XPath("//select[@id='listCondition']");
        static readonly By _lnkCheckAll = By.XPath("//a[.='Check All']");
        static readonly By _lnkUnCheckAll = By.XPath("//a[.='Uncheck All']");



        public IWebElement LnkCheckAll
        {
            get { return FindElement(_lnkCheckAll, Constant.DefaultTimeout); }
        }
        public IWebElement LnkUnCheckAll
        {
            get { return FindElement(_lnkUnCheckAll, Constant.DefaultTimeout); }
        }
        public IWebElement TxtFilterList
        {
            get { return FindElement(_txtFilterList, Constant.DefaultTimeout); }
        }
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

        #region Methods;
        /// <summary>
        /// Determines whether [is checkbox checked] [the specified chbvalue].
        /// </summary>
        /// <param name="chbvalue">The chbvalue.</param>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/11/2016 - 2:43 PM</datetime>
        public bool IsCheckboxChecked(string chbvalue)
        {
            return this.FindElement(By.XPath("//input[@value='" + chbvalue.ToLower() + "']")).Selected;
        }

        #endregion

    }
}
