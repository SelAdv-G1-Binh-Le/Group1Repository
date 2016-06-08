﻿using System;
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
    /// <datetime>6/8/2016 - 20:12</datetime>
    /// <seealso cref="Group1Project.PageObjects.GeneralPage" />
    class DataProfilesPage : GeneralPage
    {
        private IWebDriver driver;

        #region Locators

        static By _lnkAddNew = By.XPath("//a[(text()='Add New')]");
        static By _txtProfileName = By.XPath("//input[@id='txtProfileName']");
        static By _btnFinish = By.XPath("//input[@value='Finish']");


        //input[@value='Finish']
        #endregion

        #region Elements
        public IWebElement BtnFinish
        {
            get { return FindElement(_btnFinish, Constant.DefaultTimeout); }
        }
        public IWebElement TxtProfileName
        {
            get { return FindElement(_txtProfileName, Constant.DefaultTimeout); }
        }
        public IWebElement LnkAddNew
        {
            get { return FindElement(_lnkAddNew, Constant.DefaultTimeout); }
        }

        #endregion

        #region Methods

        public DataProfilesPage(IWebDriver webDriver)
            : base(webDriver)
        {
            this.driver = webDriver;
        }

        /// <summary>
        /// Adds the data profile success.
        /// </summary>
        /// <param name="dataprofilename">The dataprofilename.</param>
        /// <author>Diep Duong</author>
        /// <datetime>6/8/2016 - 20:26</datetime>
        public DataProfilesPage AddDataProfileSuccess(string dataprofilename)
        {
            LnkAddNew.Click();
            TxtProfileName.Set(dataprofilename);
            BtnFinish.Click();
            return this;
        }


        //public DataProfilesPage DeleteDataProfile(string dataprofilename)
        //{
        //    By dynamicXpath = By.XPath("//td[contains(.,'" + dataprofilename + "')]//following::a[contains(.,'Delete')]");
        //    MainPage mainpage = new MainPage(webDriver);
        //    mainpage.GotoPanelsPage().FindElement(dynamicXpath, Constant.DefaultTimeout).Click();
        //    webDriver.SwitchTo().Alert().Accept();
        //    CommonMethods.WaitForControlDisappear(webDriver, By.XPath("//table[@class='GridView']//a[text()='" + panelname + "']"), Constant.DefaultTimeout);
        //    return this;
        //}

        #endregion
    }
}
