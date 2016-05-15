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
            mainpage.ClickAddPage().AddPage("Page 1");

            IWebElementExtension.FindElement(By.XPath("//a[contains(.,'Page 1')]"));
            CommonMethods.WaitForControl(By.XPath("//a[contains(.,'Page 1')]"), Constant.DefaultTimeout);        
            
            
            
            //6	Step	Go to Global Setting -> Create Panel
            mainpage.MnGlobalSetting.Click();
            mainpage.LnkAddPanel.Click();

            //7	VP	Verify that all pre-set panels are populated and sorted correctly	



        }

       
    }
}