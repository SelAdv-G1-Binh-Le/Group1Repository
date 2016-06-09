using OpenQA.Selenium;
using Group1Project.Common;

namespace Group1Project.PageObjects
{
    class AddPageDialog : GeneralPage
    {

        private IWebDriver driver;
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
            get { return FindElement(_cmbParentPage); }
        }
        public IWebElement CmbNumberColumn
        {
            get { return FindElement(_cmbNumberColumn); }
        }
        public IWebElement CmbDisplayAfter
        {
            get { return FindElement(_cmbDisplayAfter); }
        }
        public IWebElement ChbPublic
        {
            get { return FindElement(_chbPublic); }
        }
        public IWebElement BtnCancel
        {
            get { return FindElement(_btnCancel); }
        }
        public IWebElement TxtPageName
        {
            get { return FindElement(_txtPageName); }
        }
        public IWebElement BtnOK
        {
            get { return FindElement(_btnOK); }
        }

        #endregion

        #region Methods

        public void AddPage(string pagename, string button = "OK")
        {
            IWebElementExtension.Set(this.TxtPageName, pagename, true);
            this.BtnOK.Click();
            CommonMethods.WaitForControlDisappear(webDriver, By.XPath("//div[@class='ui-dialog-overlay custom-overlay']"), Constant.DefaultTimeout);
        }


        public AddPageDialog(IWebDriver webDriver)
            : base(webDriver)
        {
            this.driver = webDriver;
        }


        #endregion
    }
}
