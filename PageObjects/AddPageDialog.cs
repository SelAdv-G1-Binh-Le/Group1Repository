using OpenQA.Selenium;
using Group1Project.Common;

namespace Group1Project.PageObjects
{
    class AddPageDialog : GeneralPage
    {

        public IWebDriver webDriver;

        #region Locators

        static readonly By _txtPageName = By.XPath("//input[@id='name']");
        static readonly By _cmbParentPage = By.XPath("//select[@id='parent']");
        static readonly By _cmbNumberColumn = By.XPath("//select[@id='columnnumber']");
        static readonly By _cmbDisplayAfter = By.XPath("//select[@id='afterpage']");
        static readonly By _chbPublic = By.XPath("//input[@id='ispublic']");
        static readonly By _btnOK = By.XPath("//input[@id='OK']");
        static readonly By _btnCancel = By.XPath("//input[@id='Cancel']");
        
                

        #endregion

        #region Elements

      

        public IWebElement CmbParentPage
        {
            get { return webDriver.FindElement(_cmbParentPage); }
        }
        public IWebElement CmbNumberColumn
        {
            get { return webDriver.FindElement(_cmbNumberColumn); }
        }
        public IWebElement CmbDisplayAfter
        {
            get { return webDriver.FindElement(_cmbDisplayAfter); }
        }
        public IWebElement ChbPublic
        {
            get { return webDriver.FindElement(_chbPublic); }
        }
        public IWebElement BtnCancel
        {
            get { return webDriver.FindElement(_btnCancel); }
        }
        public IWebElement TxtPageName
        {
            get { return webDriver.FindElement(_txtPageName); }
        }
        public IWebElement BtnOK
        {
            get { return webDriver.FindElement(_btnOK); }
        }

        #endregion

        #region Methods

        public AddPageDialog(IWebDriver webDriver)
        {
            this.webDriver = webDriver;

        }


        public void AddPage(string pagename, string button = "OK")
        {
            IWebElementExtension.Set(this.TxtPageName, pagename, true);
            this.BtnOK.Click();
            CommonMethods.WaitForControl(webDriver, By.XPath(CommonMethods.XPathContainGenerate("a", pagename)), 10);
        }


        #endregion
    }
}
