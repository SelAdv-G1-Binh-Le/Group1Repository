using OpenQA.Selenium;
using Group1Project.Common;

namespace Group1Project.PageObjects
{
    class AddPageDialog : GeneralPage
    {

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
            get { return IWebElementExtension.FindElement(_cmbParentPage); }
        }
        public IWebElement CmbNumberColumn
        {
            get { return IWebElementExtension.FindElement(_cmbNumberColumn); }
        }
        public IWebElement CmbDisplayAfter
        {
            get { return IWebElementExtension.FindElement(_cmbDisplayAfter); }
        }
        public IWebElement ChbPublic
        {
            get { return IWebElementExtension.FindElement(_chbPublic); }
        }
        public IWebElement BtnCancel
        {
            get { return IWebElementExtension.FindElement(_btnCancel); }
        }
        public IWebElement TxtPageName
        {
            get { return IWebElementExtension.FindElement(_txtPageName); }
        }
        public IWebElement BtnOK
        {
            get { return IWebElementExtension.FindElement(_btnOK); }
        }

        #endregion

        #region Methods

        public void AddPage(string pagename, string button = "OK")
        {            
            IWebElementExtension.Set(this.TxtPageName, pagename, true);
            this.BtnOK.Click();
            CommonMethods.WaitForControl(By.XPath(CommonMethods.XPathContainGenerate("a", pagename)),10);
        }


        #endregion
    }
}
