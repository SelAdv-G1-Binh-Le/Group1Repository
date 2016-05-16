using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.Common;
using Group1Project.PageObjects;
using System.Threading;
using OpenQA.Selenium;

namespace Group1Project.TestCases
{
    [TestClass]
    public class Panel : Testbase
    {
        [TestMethod]
        public void TC27()
        {
            Console.WriteLine("TC27 - Verify that when \"Choose panels\" form is expanded all pre-set panels are populated and sorted correctly");

            //1	Step	Navigate to Dashboard login page	
            //2	Step	Login with valid account	test / test
            LoginPage loginpage = new LoginPage().Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //3	Step	Go to Global Setting -> Add page
            //4	Step	Enter page name to Page Name field.	Page 1
            //5	Step	Click OK button	           

            string pagename = "Page 1";
            mainpage.ClickAddPage().AddPage(pagename);

            //6	Step	Go to Global Setting -> Create Panel
            mainpage.MnGlobalSetting.Click();
            mainpage.LnkAddPanel.Click();

            //7	VP	Verify that all pre-set panels are populated and sorted correctly	

        }

        [TestMethod]
        public void TC28()
        {
            Console.WriteLine("TC28 - Verify that when \"Add New Panel\" form is on focused all other control/form is disabled or locked.");

            //1	Step	Navigate to Dashboard login page
            //2	Step	Login with valid account
            LoginPage loginpage = new LoginPage().Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            //3	Step	Click Administer link
            //4	Step	Click Panel link
            //5	Step	Click Add New link

            IWebElementExtension.FindElement(By.XPath("//a[contains(.,'Administer')]")).Click();
            IWebElementExtension.FindElement(By.XPath("//a[@href='panels.jsp']")).Click();
            IWebElementExtension.FindElement(By.XPath("//a[contains(.,'Add New')]")).Click();

            //6	Step	Try to click other controls when Add New Panel dialog is opening
            //7	VP	Observe the current page

        }


        [TestMethod]
        public void TC29()
        {
            Console.WriteLine("TC29 - Verify that user is unable to create new panel when (*) required field is not filled");
            //1	Step	Navigate to Dashboard
            //2	Step	Select specific repository
            //3	Step	Enter valid username and password
            //4	Step	Click on Login button

            LoginPage loginpage = new LoginPage().Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //5	Step	Click on Administer/Panels link
            //6	Step	Click on "Add new" link
            //7	Step	Click on OK button

            IWebElementExtension.FindElement(By.XPath("//a[contains(.,'Administer')]")).Click();
            IWebElementExtension.FindElement(By.XPath("//a[@href='panels.jsp']")).Click();
            IWebElementExtension.FindElement(By.XPath("//a[contains(.,'Add New')]")).Click();
            IWebElementExtension.FindElement(By.XPath("//input[@id='OK']")).Click();

            //8	VP	Check warning message show up.
            string alerttext = CommonMethods.CloseAlertAndGetItsText(TestCases.Testbase.WebDriver);
            VP.CheckText("Display Name is a required field.", alerttext);

        }
        [TestMethod]
        public void TC30()
        {
            Console.WriteLine("TC30 - Verify that no special character except '@' character is allowed to be inputted into \"Display Name\" field");

            //1	Step	Navigate to Dashboard login page
            //2	Step	Login with valid account
            //3	Step	Click Administer link
            //4	Step	Click Panel link
            //5	Step	Click Add New link
            LoginPage loginpage = new LoginPage().Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            IWebElementExtension.FindElement(By.XPath("//a[contains(.,'Administer')]")).Click();
            IWebElementExtension.FindElement(By.XPath("//a[@href='panels.jsp']")).Click();
            IWebElementExtension.FindElement(By.XPath("//a[contains(.,'Add New')]")).Click();

            //6	Step	Enter value into Display Name field with special characters except "@"
            //7	Step	Click Ok button
            IWebElementExtension.FindElement(By.XPath("//input[@id='txtDisplayName']")).SendKeys("Logigear#$%");
            IWebElementExtension.FindElement(By.XPath("//input[@id='OK']")).Click();

            //8	VP	Observe the current page
            //9	Step	Close Warning Message box
            string alerttext = CommonMethods.CloseAlertAndGetItsText(TestCases.Testbase.WebDriver);
            //VP.CheckText("Invalid display name. The name can't contain high ASCII characters or any of following characters: /:*?<>|\"#{[]{};", alerttext);
            Thread.Sleep(3000);
            IWebElementExtension.FindElement(By.XPath(" //input[@id='Cancel']")).Click();

            //10	Step	Click Add New link
            //11	Step	Enter value into Display Name field with special character is @
            //12	VP	Observe the current page
            IWebElementExtension.FindElement(By.XPath("//a[contains(.,'Add New')]")).Click();
            IWebElementExtension.FindElement(By.XPath("//input[@id='txtDisplayName']")).SendKeys("Logigear@");
            IWebElementExtension.FindElement(By.XPath("//input[@id='OK']")).Click();
        }


    }
}