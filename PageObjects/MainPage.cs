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
    class MainPage : GeneralPage
    {

        public IWebDriver webDriver;

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
            get { return webDriver.FindElement(_lnkAddPanel); }
        }
        public IWebElement LnkAddPage
        {
            get { return webDriver.FindElement(_lnkAddPage); }
        }

        public IWebElement MnMainBar
        {
            get { return webDriver.FindElement(_mnMainBar); }
        }
        public IWebElement TxtPageName
        {
            get { return webDriver.FindElement(_txtPageName); }
        }
        public IWebElement BtnPopupOk
        {
            get { return webDriver.FindElement(_btnPopupOk); }
        }
        public IWebElement MnGlobalSetting
        {
            get { return webDriver.FindElement(_mnGlobalSetting); }
        }
        public IWebElement LblWelcome
        {
            get { return webDriver.FindElement(_lblWelcome); }
        }

        public IWebElement LnkLogout
        {
            get { return webDriver.FindElement(_lnkLogout); }
        }


        public IWebElement LblRepository
        {
            get { return webDriver.FindElement(_lblRepository); }
        }


        #endregion

        #region Methods

          public MainPage (IWebDriver webDriver)
        {
            this.webDriver = webDriver;
                        
        }
                

          public LoginPage Logout()
        {
            this.LblWelcome.Click();
            this.LnkLogout.Click();
            return new LoginPage(webDriver);
        }

        public MainPage ChangeRepository(string repository)
        {
            Console.WriteLine("ChangeRepository " + repository);
            this.LblRepository.Click();
            IWebElement DynamiclblRepository = webDriver.FindElement(By.XPath("//ul[@id='ulListRepositories']//a[contains(.,'" + repository + "')]"));
            DynamiclblRepository.Click();

            CommonMethods.WaitForControl(webDriver, By.XPath("//span[contains(.,'" + repository + "')]"), 5);


            return this;
        }

        public void SelectChildMenu(MenuList.MainMenuEnum main, MenuList.ChildMenuEnum child)
        {
            WebDriverWait wait = new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(String.Format("//{0}", MenuList.returnMainMenu(main)))));
            IWebElement ParentLink = this.webDriver.FindElement(By.XPath(String.Format("//{0}", MenuList.returnMainMenu(main))));
            Actions action = new Actions(this.webDriver);
            action.MoveToElement(ParentLink).Perform();
            IWebElement ChildLink = this.webDriver.FindElement(By.XPath(String.Format("//a[.='{0}']", MenuList.returnChildMenu(child))));
            ChildLink.Click();
        }
        public void SelectChildPage(string parent, string child)
        {
            WebDriverWait wait = new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(.,'" + parent + "')]")));
            IWebElement ParentLink = this.webDriver.FindElement(By.XPath("//a[contains(.,'" + parent + "')]"));
            Actions action = new Actions(this.webDriver);
            action.MoveToElement(ParentLink).Perform();
            IWebElement ChildLink = this.webDriver.FindElement(By.XPath("//a[contains(.,'" + child + "')]"));
            ChildLink.Click();
        }
        public void SelectChildPage(string parent, string mid, string child)
        {
            WebDriverWait wait = new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(.,'"+parent+"')]")));
            IWebElement ParentLink = this.webDriver.FindElement(By.XPath("//a[contains(.,'" + parent + "')]"));
            Actions action = new Actions(this.webDriver);
            action.MoveToElement(ParentLink).Perform();
            IWebElement MidLink = this.webDriver.FindElement(By.XPath("//a[contains(.,'" + mid + "')]"));
            action.MoveToElement(MidLink).Perform();
            IWebElement ChildLink = this.webDriver.FindElement(By.XPath("//a[contains(.,'" + child + "')]"));
            ChildLink.Click();
        }
        public int GetTabIndex(string tabname)
        {
            string xpath = string.Format("//div[@id='main-menu']//a[.='{0}']/../preceding-sibling::li", tabname);
            int tabindex = webDriver.FindElements(By.XPath(xpath)).Count();
            return tabindex+1;
        }

        public void AddOrEditPage(string name, string parent, string column, string after, bool status, string clickbutton)
        {
            bool checkpopupexist = CommonMethods.IsElementPresent(webDriver,By.XPath("//input[@id='name']"));
            if (checkpopupexist!=true)
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(2));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@class='mn-setting']")));
                this.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.AddPage);
            }
            IWebElement namebox = webDriver.FindElement(By.XPath("//input[@id='name']"));
            if(name!="")
            {
                namebox.Clear();
                namebox.SendKeys(name);
            }

            
            if (parent != "")
            {
                //CommonMethods.WaitAndClickControl("select", "@id", "parent", parent);
                CommonMethods.WaitAndClickControl(webDriver,"option", ".", parent, "");
                Thread.Sleep(500);
            }
            if (column != "")
            {
                CommonMethods.WaitAndClickControl(webDriver,"select", "@id", "columnnumber", column);
            }
            if (after != "")
            {
                CommonMethods.WaitAndClickControl(webDriver, "select", "@id", "afterpage", after);
            }
            if (status.ToString() != "")
            {
                IWebElement checkbox = webDriver.FindElement(By.XPath("//input[@id='ispublic']"));
                if(status==true & checkbox.Selected==false)
                {
                    checkbox.Click();
                }
                if(status==false & checkbox.Selected==true)
                {
                    checkbox.Click();
                }
            }
            {
                CommonMethods.WaitAndClickControl(webDriver, "input", "@id", clickbutton, "");
            }
            Thread.Sleep(500);
        }
        public void DeletePage(string pagename)
        {
            CommonMethods.WaitAndClickControl(webDriver, "a", "text()", pagename, "");
            this.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Delete);
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = webDriver.SwitchTo().Alert();
            alert.Accept();
            webDriver.SwitchTo().DefaultContent();
        }
        public void DeletePage(string parentpage, string childpage)
        {
            this.SelectChildPage(parentpage, childpage);
            this.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Delete);
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = webDriver.SwitchTo().Alert();
            alert.Accept();
            webDriver.SwitchTo().DefaultContent();
            Thread.Sleep(500);
        }
        public void DeletePage(string parentpage, string midpage, string childpage)
        {
            this.SelectChildPage(parentpage, midpage, childpage);
            this.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Delete);
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = webDriver.SwitchTo().Alert();
            alert.Accept();
            webDriver.SwitchTo().DefaultContent();
            Thread.Sleep(500);
        }

        public bool IsTabVisible(string tabname)
        {
            return CommonMethods.IsElementPresent(webDriver, OpenQA.Selenium.By.XPath("//a[.='" + tabname + "']"));
        }
        public void ClickTab(string tabname)
        {
            CommonMethods.WaitAndClickControl(webDriver, "a", "text()", tabname, "");
        }
        public AddPageDialog ClickAddPage()
        {
            AddPageDialog dialog = new AddPageDialog(this.webDriver);
            this.MnGlobalSetting.Click();
            this.LnkAddPage.Click();
            return dialog;
        }
        public string GetAlertMessage()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = webDriver.SwitchTo().Alert();
            string alerttext = alert.Text;
            alert.Accept();
            webDriver.SwitchTo().DefaultContent();
            return alerttext;
        }

        public string GetParentPage(string childpage)
        {
            IWebElement element = webDriver.FindElement(By.XPath("//a[.='" + childpage + "']/ancestor::li/a[contains(@class,'haschild')]"));
            return element.Text;
        }



        #endregion

    }
}
