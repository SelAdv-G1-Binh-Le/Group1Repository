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
        private IWebDriver driver;

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
        static readonly By _btnChoosepanel = By.XPath("//a[@id='btnChoosepanel']");
        static readonly By _dlgOverlay = By.XPath("//div[@class='ui-dialog-overlay custom-overlay']");
        static readonly By _lnkAdminister = By.XPath("//a[@href='#Administer']");
        static readonly By _lnkPanels = By.XPath("//a[@href='panels.jsp']");
        static readonly By _lnkMenuSettingDelete = By.XPath("//li[@class='mn-setting']//a[@class='delete']");
        static readonly By _cbbPages = By.XPath("//div[@id='div_panelConfigurationDlg']//select[@id='cbbPages']");
        static readonly By _btnCreateNewPanel = By.XPath("//span[text()='Create new panel']");
        static readonly By _divChoosePanelsOpen = By.XPath("//div[@class='ccpanels'][@style='display: block;']");
        

        //span[text()='Create new panel']
        #endregion

        #region Elements

        public IWebElement DivChoosePanelsOpen
        {
            get { return FindElement(_divChoosePanelsOpen, Constant.DefaultTimeout); }
        }

        public IWebElement BtnCreateNewPanel
        {
            get { return FindElement(_btnCreateNewPanel, Constant.DefaultTimeout); }
        }
        public IWebElement CbbPages
        {
            get { return FindElement(_cbbPages, Constant.DefaultTimeout); }
        }
        public IWebElement LnkMenuSettingDelete
        {
            get { return FindElement(_lnkMenuSettingDelete, Constant.DefaultTimeout); }
        }

        public IWebElement LnkPanels
        {
            get { return FindElement(_lnkPanels, Constant.DefaultTimeout); }
        }

        public IWebElement LnkAdminister
        {
            get { return FindElement(_lnkAdminister, Constant.DefaultTimeout); }
        }
        public IWebElement DlgOverlay
        {
            get { return FindElement(_dlgOverlay, Constant.DefaultTimeout); }
        }

        public IWebElement BtnChoosepanel
        {
            get { return FindElement(_btnChoosepanel, Constant.DefaultTimeout); }
        }
        public IWebElement LnkAddPanel
        {
            get { return FindElement(_lnkAddPanel, Constant.DefaultTimeout); }
        }
        public IWebElement LnkAddPage
        {
            get { return FindElement(_lnkAddPage, Constant.DefaultTimeout); }
        }

        public IWebElement MnMainBar
        {
            get { return FindElement(_mnMainBar, Constant.DefaultTimeout); }
        }
        public IWebElement TxtPageName
        {
            get { return FindElement(_txtPageName, Constant.DefaultTimeout); }
        }
        public IWebElement BtnPopupOk
        {
            get { return FindElement(_btnPopupOk, Constant.DefaultTimeout); }
        }
        public IWebElement MnGlobalSetting
        {
            get { return FindElement(_mnGlobalSetting, Constant.DefaultTimeout); }
        }
        public IWebElement LblWelcome
        {
            get { return FindElement(_lblWelcome, Constant.DefaultTimeout); }
        }

        public IWebElement LnkLogout
        {
            get { return FindElement(_lnkLogout, Constant.DefaultTimeout); }
        }


        public IWebElement LblRepository
        {
            get { return FindElement(_lblRepository, Constant.DefaultTimeout); }
        }


        #endregion

        #region Methods

        public MainPage(IWebDriver webDriver)
            : base(webDriver)
        {
            this.driver = webDriver;
        }


        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns></returns>
        public LoginPage Logout()
        {
            this.LblWelcome.Click();
            this.LnkLogout.Click();
            return new LoginPage(webDriver);
        }

        /// <summary>
        /// Changes the repository.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <returns></returns>
        public MainPage ChangeRepository(string repository)
        {
            Console.WriteLine("ChangeRepository " + repository);
            this.LblRepository.Click();
            IWebElement DynamiclblRepository = webDriver.FindElement(By.XPath("//ul[@id='ulListRepositories']//a[contains(.,'" + repository + "')]"));
            DynamiclblRepository.Click();

            CommonMethods.WaitForControl(webDriver, By.XPath("//span[contains(.,'" + repository + "')]"), 5);


            return this;
        }


        /// <summary>
        /// Selects the child menu.
        /// </summary>
        /// <param name="main">The main.</param>
        /// <param name="child">The child.</param>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:05 AM</datetime>
        public void SelectChildMenu(MenuList.MainMenuEnum main, MenuList.ChildMenuEnum child)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(String.Format("//{0}", MenuList.returnMainMenu(main)))));
            IWebElement ParentLink = this.FindElement(By.XPath(String.Format("//{0}", MenuList.returnMainMenu(main))), Constant.DefaultTimeout);
            ParentLink.Click();
            //Actions action = new Actions(this.webDriver);
            //action.MoveToElement(ParentLink).Perform();
            IWebElement ChildLink = this.FindElement(By.XPath(String.Format("//a[.='{0}']", MenuList.returnChildMenu(child))), Constant.DefaultTimeout);
            ChildLink.Click();
        }

        /// <summary>
        /// Selects the child page.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The child.</param>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:06 AM</datetime>
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

        /// <summary>
        /// Selects the child page.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="mid">The mid.</param>
        /// <param name="child">The child.</param>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:06 AM</datetime>
        public void SelectChildPage(string parent, string mid, string child)
        {
            WebDriverWait wait = new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(.,'" + parent + "')]")));
            IWebElement ParentLink = this.webDriver.FindElement(By.XPath("//a[contains(.,'" + parent + "')]"));
            Actions action = new Actions(this.webDriver);
            action.MoveToElement(ParentLink).Perform();
            IWebElement MidLink = this.webDriver.FindElement(By.XPath("//a[contains(.,'" + mid + "')]"));
            action.MoveToElement(MidLink).Perform();
            IWebElement ChildLink = this.webDriver.FindElement(By.XPath("//a[contains(.,'" + child + "')]"));
            ChildLink.Click();
        }

        /// <summary>
        /// Gets the index of the tab.
        /// </summary>
        /// <param name="tabname">The tabname.</param>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:06 AM</datetime>
        public int GetTabIndex(string tabname)
        {
            string xpath = string.Format("//div[@id='main-menu']//a[.='{0}']/../preceding-sibling::li", tabname);
            int tabindex = webDriver.FindElements(By.XPath(xpath)).Count();
            return tabindex + 1;
        }


        /// <summary>
        /// Adds the page.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="column">The column.</param>
        /// <param name="after">The after.</param>
        /// <param name="status">if set to <c>true</c> [status].</param>
        /// <param name="clickbutton">The clickbutton.</param>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:06 AM</datetime>
        public MainPage AddPage(string name, string parent = "", string column = "", string after = "", bool status = false, string clickbutton = "OK")
        {
            this.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.AddPage);
            IWebElement namebox = this.FindElement(By.XPath("//input[@id='name']"), Constant.DefaultTimeout);
            if (name != "")
            {
                namebox.Clear();
                namebox.SendKeys(name);
            }
            if (status.ToString() != "")
            {
                IWebElement checkbox = this.FindElement(By.XPath("//input[@id='ispublic']"), Constant.DefaultTimeout);
                if (status == true & checkbox.Selected == false)
                {
                    checkbox.Click();
                }
                if (status == false & checkbox.Selected == true)
                {
                    checkbox.Click();
                }
            }
            if (parent != "")
            {
                this.FindElement(By.XPath("//select[@id='parent']/option[contains(.,'" + parent + "')]"), Constant.DefaultTimeout).Click();
                //Thread.Sleep(500);
            }
            if (column != "")
            {
                SelectElement box = new SelectElement(this.FindElement(By.XPath("//select[@id='columnnumber']"), Constant.DefaultTimeout));
                box.SelectByText(column);
            }
            if (after != "")
            {
                this.FindElement(By.XPath("//select[@id='afterpage']/option[contains(.,'" + after + "')]"), Constant.DefaultTimeout).Click();
            }

            if (clickbutton != "")
            {
                this.FindElement(By.XPath("//input[@id='" + clickbutton + "']"), Constant.DefaultTimeout).Click();
            }
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
                wait.Until(ExpectedConditions.ElementExists(By.XPath("//a[.='" + this.ConvertBlankCharacter(name) + "']")));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return this;
        }
        /// <summary>
        /// Edits the page.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="column">The column.</param>
        /// <param name="after">The after.</param>
        /// <param name="status">if set to <c>true</c> [status].</param>
        /// <param name="clickbutton">The clickbutton.</param>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:06 AM</datetime>
        public void EditPage(string name, string parent = "", string column = "", string after = "", bool status = false, string clickbutton = "OK")
        {
            IWebElement namebox = this.FindElement(By.XPath("//input[@id='name']"), Constant.DefaultTimeout);
            if (name != "")
            {
                namebox.Clear();
                namebox.SendKeys(name);
            }
            if (parent != "")
            {
                //CommonMethods.WaitAndClickControl("select", "@id", "parent", parent);
                //CommonMethods.WaitAndClickControl(webDriver, "option", ".", parent, "");
                this.FindElement(By.XPath("//select[@id='parent']/option[contains(.,'" + parent + "')]"), Constant.DefaultTimeout).Click();
                //Thread.Sleep(500);
            }
            if (column != "")
            {
                SelectElement box = new SelectElement(this.FindElement(By.XPath("//select[@id='columnnumber']"), Constant.DefaultTimeout));
                box.SelectByText(column);
            }
            if (after != "")
            {
                //CommonMethods.WaitAndClickControl(webDriver, "option", ".", after, "");
                this.FindElement(By.XPath("//select[@id='afterpage']/option[contains(.,'" + after + "')]"), Constant.DefaultTimeout).Click();
            }
            if (status.ToString() != "")
            {
                IWebElement checkbox = this.FindElement(By.XPath("//input[@id='ispublic']"), Constant.DefaultTimeout);
                if (status == true & checkbox.Selected == false)
                {
                    checkbox.Click();
                }
                if (status == false & checkbox.Selected == true)
                {
                    checkbox.Click();
                }
            }
            if (clickbutton != "")
            {
                CommonMethods.WaitAndClickControl(webDriver, "input", "@id", clickbutton, "");
                this.FindElement(By.XPath("//input[@id='" + clickbutton + "']"), Constant.DefaultTimeout);
            }
            //Thread.Sleep(500);
        }


        /// <summary>
        /// Deletes the page.
        /// </summary>
        /// <param name="pagename">The pagename.</param>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:06 AM</datetime>
        public MainPage DeletePage(string pagename)
        {
            this.SelectPage(pagename);
            //this.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Delete);
            this.MnGlobalSetting.Click();
            this.LnkMenuSettingDelete.Click();
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = webDriver.SwitchTo().Alert();
            alert.Accept();
            webDriver.SwitchTo().DefaultContent();
            CommonMethods.WaitForControlDisappear(webDriver, By.XPath("//a[contains(@href,'/TADashboard/')][contains(.,'" + this.ConvertBlankCharacter(pagename) + "')]"), Constant.DefaultTimeout);
            return this;
        }

        /// <summary>
        /// Deletes the page.
        /// </summary>
        /// <param name="parentpage">The parentpage.</param>
        /// <param name="childpage">The childpage.</param>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:07 AM</datetime>
        public void DeletePage(string parentpage, string childpage)
        {
            this.SelectChildPage(this.ConvertBlankCharacter(parentpage), this.ConvertBlankCharacter(childpage));
            this.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Delete);
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = webDriver.SwitchTo().Alert();
            alert.Accept();
            webDriver.SwitchTo().DefaultContent();
            Thread.Sleep(500);
        }

        /// <summary>
        /// Deletes the page.
        /// </summary>
        /// <param name="parentpage">The parentpage.</param>
        /// <param name="midpage">The midpage.</param>
        /// <param name="childpage">The childpage.</param>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:07 AM</datetime>
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


        /// <summary>
        /// Determines whether [is tab visible] [the specified tabname].
        /// </summary>
        /// <param name="tabname">The tabname.</param>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:07 AM</datetime>
        public bool IsTabVisible(string tabname)
        {
            return CommonMethods.IsElementPresent(webDriver, OpenQA.Selenium.By.XPath("//a[.='" + this.ConvertBlankCharacter(tabname) + "']"));
        }

        /// <summary>
        /// Clicks the tab.
        /// </summary>
        /// <param name="tabname">The tabname.</param>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:07 AM</datetime>
        public void ClickTab(string tabname)
        {
            //CommonMethods.WaitAndClickControl(webDriver, "a", "text()", this.ConvertBlankCharacter(tabname), "");
            this.FindElement(By.XPath("//a[.='" + this.ConvertBlankCharacter(tabname) + "']"), Constant.DefaultTimeout).Click();
        }

        /// <summary>
        /// Clicks the add page.
        /// </summary>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:07 AM</datetime>
        public AddPageDialog ClickAddPage()
        {
            AddPageDialog dialog = new AddPageDialog(webDriver);
            this.MnGlobalSetting.Click();
            this.LnkAddPage.Click();
            return dialog;
        }

        /// <summary>
        /// Gets the alert message.
        /// </summary>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:07 AM</datetime>
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


        /// <summary>
        /// Gets the parent page.
        /// </summary>
        /// <param name="childpage">The childpage.</param>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:08 AM</datetime>
        public string GetParentPage(string childpage)
        {
            IWebElement element = webDriver.FindElement(By.XPath("//a[.='" + childpage + "']/ancestor::li/a[contains(@class,'haschild')]"));
            return element.Text;
        }


        /// <summary>
        /// Waits for overlay disappear.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <author>Diep Duong</author>
        /// <datetime>6/2/2016 - 03:25</datetime>
        public void WaitForOverlayDisappear(int timeout)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(15));
            wait.Until((d) =>
            {
                return d.FindElements(By.XPath("//div[@class='ui-dialog-overlay custom-overlay']")).Count == 0;
            });

        }


        /// <summary>
        /// Gets the page column.
        /// </summary>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:08 AM</datetime>
        public int GetPageColumn()
        {
            string xPath = "//div[@id='columns']/ul[contains(@id,'column')]";
            return webDriver.FindElements(By.XPath(xPath)).Count - 1;
        }
        public string ConvertBlankCharacter(string text)
        {
            return text.Replace(" ", "\u00A0");
        }


        /// <summary>
        /// Gets the profile data table cell value.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:08 AM</datetime>
        public string GetProfileDataTableCellValue(string column, string row)
        {
            string convertedRowText = this.ConvertBlankCharacter(row);
            string xPath = string.Format("//table[@class='GridView']//tr[count(//td[.='{1}']/../preceding-sibling::tr)+1]/td[count(//th[.='{0}']/preceding-sibling::th)+1]", column, convertedRowText);
            return webDriver.FindElement(By.XPath(xPath)).Text;
        }

        /// <summary>
        /// Gets all value of column.
        /// </summary>
        /// <param name="colname">The colname.</param>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:08 AM</datetime>
        public string GetAllValueOfColumn(string colname)
        {
            int totalRow = webDriver.FindElements(By.XPath("//a[.='Save as']")).Count;
            string totalString = "";
            for (int i = 2; i < totalRow + 2; i++)
            {
                string xPath = string.Format("//table[@class='GridView']//tr[{1}]/td[count(//th[.='{0}']/preceding-sibling::th)+1]", colname, i);
                string value = webDriver.FindElement(By.XPath(xPath)).Text;
                totalString += value;
            }
            Console.WriteLine(totalString);
            return totalString;
        }
        /// <summary>
        /// Checks the data profile sort.
        /// </summary>
        /// <param name="colname">The colname.</param>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/8/2016 - 2:03 AM</datetime>
        public bool CheckDataProfileSort(string colname)
        {
            int totalRow = webDriver.FindElements(By.XPath("//a[.='Save as']")).Count;
            bool checkSort = true;
            for (int i = 2; i < totalRow + 1; i++)
            {
                string xPath1 = string.Format("//table[@class='GridView']//tr[{1}]/td[count(//th[.='{0}']/preceding-sibling::th)+1]", colname, i);
                string xPath2 = string.Format("//table[@class='GridView']//tr[{1}]/td[count(//th[.='{0}']/preceding-sibling::th)+1]", colname, i+1);
                string value1 = webDriver.FindElement(By.XPath(xPath1)).Text;
                string value2 = webDriver.FindElement(By.XPath(xPath2)).Text;
                if(value2.CompareTo(value1)>0)
                {
                    checkSort = true;
                }
                else
                {
                    checkSort = false;
                    break;
                }
            }
            Console.WriteLine(checkSort);
            return checkSort;
        }

        /// <summary>
        /// Gets all value of column.
        /// </summary>
        /// <param name="colindex">The colindex.</param>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 3:09 AM</datetime>
        public string GetAllValueOfColumn(int colindex)
        {
            int totalRow = webDriver.FindElements(By.XPath("//a[.='Save as']")).Count;
            string totalString = "";
            for (int i = 2; i < totalRow + 2; i++)
            {
                string xPath = string.Format("//table[@class='GridView']//tr[{1}]/td[{0}]", colindex, i);
                string value = webDriver.FindElement(By.XPath(xPath)).Text;
                totalString += value;
            }
            Console.WriteLine(totalString);
            return totalString;
        }

        /// <summary>
        /// Adds the data profile.
        /// </summary>
        /// <param name="profilename">The profilename.</param>
        /// <param name="clickbutton">The clickbutton.</param>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/8/2016 - 2:02 AM</datetime>
        public MainPage AddDataProfile(string profilename,string clickbutton)
        {
            this.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.CreateProfile);
            this.FindElement(By.XPath("//input[@id='txtProfileName']")).SendKeys(profilename);
            this.FindElement(By.XPath("//input[@class='button' and @value='"+clickbutton+"']")).Click();
            CommonMethods.WaitForControl(webDriver, By.XPath("//td[.='" + this.ConvertBlankCharacter(profilename) + "']"),Constant.DefaultTimeout);
            return this;
        }

        /// <summary>
        /// Determines whether [is CheckBox on row] [the specified rowtext].
        /// </summary>
        /// <param name="rowtext">The rowtext.</param>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/8/2016 - 2:02 AM</datetime>
        public bool IsCheckBoxOnRow(string rowtext)
        {
            string xPath = string.Format("//td[.='{0}']/preceding-sibling::td/input[@type='checkbox']", this.ConvertBlankCharacter(rowtext));
            return CommonMethods.IsElementPresent(webDriver, By.XPath(xPath));
        }

        /// <summary>
        /// Deletes the data profile.
        /// </summary>
        /// <param name="profilename">The profilename.</param>
        /// <returns></returns>
        /// <author>Binh Le</author>
        /// <datetime>6/8/2016 - 2:02 AM</datetime>
        public MainPage DeleteDataProfile(string profilename)
        {
            bool isPageExist = CommonMethods.IsElementPresent(webDriver, By.XPath("//table[@class='GridView']"));
            if(isPageExist!=true)
            {
                this.SelectChildMenu(MenuList.MainMenuEnum.Administer, MenuList.ChildMenuEnum.DataProfiles);
            }
            string xPath = string.Format("//td[.='{0}']/preceding-sibling::td/input[@type='checkbox']", this.ConvertBlankCharacter(profilename));
            this.FindElement(By.XPath(xPath)).Click();
            this.FindElement(By.XPath("//div[@class='panel_tag2']/a[.='Delete']")).Click();
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = webDriver.SwitchTo().Alert();
            alert.Accept();
            webDriver.SwitchTo().DefaultContent();
            CommonMethods.WaitForControlDisappear(webDriver, By.XPath(xPath), 1);
            return this;
        }

        /// <summary>
        /// Gotoes the panels page.
        /// </summary>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/2/2016 - 05:03</datetime>
        public PanelsPage GotoPanelsPage()
        {
            this.LnkAdminister.MoveMouse(webDriver);
            CommonMethods.WaitForControl(webDriver, _lnkPanels, Constant.DefaultTimeout);
            this.LnkPanels.Click();
            return new PanelsPage(webDriver);
        }

        /// <summary>
        /// Opens the choose panels.
        /// </summary>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/7/2016 - 03:35</datetime>
        public MainPage OpenChoosePanels()
        {
            this.BtnChoosepanel.Click();
            CommonMethods.WaitForControl(webDriver, _divChoosePanelsOpen, Constant.DefaultTimeout);            
            return this;
        }

        /// <summary>
        /// Selects the page.
        /// </summary>
        /// <param name="pagename">The pagename.</param>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/7/2016 - 06:03</datetime>
        public MainPage SelectPage(string pagename)
        {
            this.FindElement(By.XPath("//a[contains(@href,'/TADashboard/')][contains(.,'" + pagename.Replace(" ", "\u00A0") + "')]"), Constant.DefaultTimeout).Click();
            return this;
        }

        /// <summary>
        /// Chooses the panel.
        /// </summary>
        /// <param name="panelname">The panelname.</param>
        /// <returns></returns>
        /// <author>Diep Duong</author>
        /// <datetime>6/8/2016 - 02:36</datetime>
        public PanelConfigurationDialog ChoosePanel(string panelname)
        {
            this.OpenChoosePanels().FindElement(By.XPath("//a[contains(@onclick,'Dashboard.showPanelConfiguration')][contains(.,'" + panelname.Replace(" ", "\u00A0") + "')]"), Constant.DefaultTimeout).Click();
            return new PanelConfigurationDialog(webDriver); ;
        }


        #endregion

    }
}
