using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Group1Project.Common;
using Group1Project.DataObjects;
using Group1Project.TestCases;
using System.Threading;

namespace Group1Project.PageObjects
{
    class MainPage : GeneralPage
    {

        //string dynamicRepository = Constant.Repository2;

        #region Locators

        static readonly By _lblWelcome = By.XPath("//a[@href='#Welcome']");
        static readonly By _lblRepository = By.XPath("//a[@href='#Repository']");
        static readonly By _lnkLogout = By.XPath("//a[@href='logout.do']");
        static readonly By _mnGlobalSetting = By.XPath("//li[@class='mn-setting']");
        static readonly By _txtPageName = By.XPath("//input[@id='name']");
        static readonly By _btnPopupOk = By.XPath("//input[@id='OK']");
        static readonly By _mnMainBar = By.XPath("//div[@id='main-menu']");
        static readonly By _lnkAddPage = By.XPath("//a[contains(@href,'openAddPageForm')]");
        static readonly By _lnkAddPanel = By.XPath("//a[contains(@onclick,'openAddPanel')]");

        #endregion

        #region Elements

        public IWebElement LnkAddPanel
        {
            get { return IWebElementExtension.FindElement(_lnkAddPanel); }
        }
        public IWebElement LnkAddPage
        {
            get { return IWebElementExtension.FindElement(_lnkAddPage); }
        }

        public IWebElement MnMainBar
        {
            get { return IWebElementExtension.FindElement(_mnMainBar); }
        }
        public IWebElement TxtPageName
        {
            get { return IWebElementExtension.FindElement(_txtPageName); }
        }
        public IWebElement BtnPopupOk
        {
            get { return IWebElementExtension.FindElement(_btnPopupOk); }
        }
        public IWebElement MnGlobalSetting
        {
            get { return IWebElementExtension.FindElement(_mnGlobalSetting); }
        }
        public IWebElement LblWelcome
        {
            get { return IWebElementExtension.FindElement(_lblWelcome); }
        }

        public IWebElement LnkLogout
        {
            get { return IWebElementExtension.FindElement(_lnkLogout); }
        }


        public IWebElement LblRepository
        {
            get { return IWebElementExtension.FindElement(_lblRepository); }
        }


        #endregion

        #region Methods

        public LoginPage Logout()
        {
            this.LblWelcome.Click();
            this.LnkLogout.Click();
            return new LoginPage();
        }

        public MainPage ChangeRepository(string repository)
        {
            this.LblRepository.Click();
            IWebElement DynamiclblRepository = IWebElementExtension.FindElement(By.XPath("//ul[@id='ulListRepositories']//a[contains(.,'" + repository + "')]"));
            DynamiclblRepository.Click();
            return this;
        }

        public void SelectChildMenu(MenuList.MainMenuEnum main, MenuList.ChildMenuEnum child)
        {
            WebDriverWait wait = new WebDriverWait(Testbase.WebDriver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(String.Format("//{0}", MenuList.returnMainMenu(main)))));
            IWebElement ParentLink =  Testbase.WebDriver.FindElement(By.XPath(String.Format("//{0}", MenuList.returnMainMenu(main))));
            ParentLink.Click();
            IWebElement ChildLink = Testbase.WebDriver.FindElement(By.XPath(String.Format("//a[.='{0}']", MenuList.returnChildMenu(child))));
            ChildLink.Click();
        }
        public int GetTabIndex(string tabname)
        {
            string xpath = string.Format("//div[@id='main-menu']//a[.='{0}']/../preceding-sibling::li", tabname);
            int tabindex = Testbase.WebDriver.FindElements(By.XPath(xpath)).Count();
            return tabindex+1;
        }

        public void AddNewPage(string name, string parent, string column, string after, bool status, string clickbutton)
        {
            WebDriverWait wait = new WebDriverWait(Testbase.WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@class='mn-setting']")));
            this.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.AddPage);
            IWebElement namebox = Testbase.WebDriver.FindElement(By.XPath("//input[@id='name']"));
            namebox.SendKeys(name);
            if (parent != "")
            {
                CommonMethods.WaitAndClickControl("select", "@id", "parent", parent);
                Thread.Sleep(500);
            }
            if (column != "")
            {
                CommonMethods.WaitAndClickControl("select", "@id", "columnnumber", column);
            }
            if (after != "")
            {
                CommonMethods.WaitAndClickControl("select", "@id", "afterpage", after);
            }
            if (status != false)
            {
                CommonMethods.WaitAndClickControl("input", "@id", "ispublic","");
            }
            if (clickbutton != "")
            {
                CommonMethods.WaitAndClickControl("input", "@id", clickbutton,"");
            }
        }
        public void DeletePage(string pagename)
        {
            IWebElement pagetab = Testbase.WebDriver.FindElement(By.XPath("//a[.='"+pagename+"']"));
            pagetab.Click();
            this.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Delete);
            WebDriverWait wait = new WebDriverWait(Testbase.WebDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = Testbase.WebDriver.SwitchTo().Alert();
            alert.Accept();
            Testbase.WebDriver.SwitchTo().DefaultContent();
        }
        public void DeletePage(string parentpage, string childpage)
        {
            CommonMethods.WaitAndClickControl("a", "text()", parentpage,"");
            CommonMethods.WaitAndClickControl("a", "text()", childpage, "");
            this.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Delete);
            WebDriverWait wait = new WebDriverWait(Testbase.WebDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = Testbase.WebDriver.SwitchTo().Alert();
            alert.Accept();
            Testbase.WebDriver.SwitchTo().DefaultContent();
            Thread.Sleep(500);
        }

        public bool CheckTabExist(string tabname)
        {
            return CommonMethods.IsElementPresent(OpenQA.Selenium.By.XPath("//a[.='" + tabname + "']"));
        }

        public Dialog ClickAddPage()
        {
            Dialog dialog = new Dialog();
            this.MnGlobalSetting.Click();
            this.LnkAddPage.Click();
            return dialog;
        }


        #endregion

    }
}
