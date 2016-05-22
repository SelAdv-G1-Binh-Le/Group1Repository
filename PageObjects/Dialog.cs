using OpenQA.Selenium;
using Group1Project.Common;

namespace Group1Project.PageObjects
{
    class AddPageDialog : GeneralPage
    {

        #region Locators

        static readonly By _txtPageName = By.XPath("//input[@id='name']");
        static readonly By _btnOK = By.XPath("//input[@id='OK']");
        static readonly By _btnCancel = By.XPath("//input[@id='Cancel']");

        #endregion

        #region Elements

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
