using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.PageObjects;
using Group1Project.Common;

namespace Group1Project.TestCases
{

    [TestClass]
    public class MainPageTestCases : Testbase
    {
        [TestMethod]
        public void TC11()
        {
            Console.WriteLine("Verify that user is unable open more than 1 \"New Page\" dialog");
            //Step1	Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage().Open();

            //Step2 Login with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername,Constant.DefaultPassword,Constant.DefaultRepository);

            //Step3	Go to Global Setting -> Add page
            mainPage.ClickDropdownMenu(mainPage.MnGlobalSetting, "Add Page");

            //Step4	Try to go to Global Setting -> Add page again
            //mainPage.ClickDropdownMenu(mainPage.MnGlobalSetting, "Add Page");

            //VP. Observe the current page
            // User cannot go to Global Setting -> Add page while "New Page" dialog appears.
            string ActualStatus = mainPage.MnGlobalSetting.Selected.ToString();
            string ExpectedStatus = "False";
            Assert.AreEqual(ExpectedStatus, ActualStatus);
            // Post-Condition: Logout, Close TA Dashboard
            mainPage.Logout();
        }

        [TestMethod]
        public void TC12()
        {
            Console.WriteLine("Verify that user is able to add additional pages besides \"Overview\" page successfully");
            //Step1	Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage().Open();

            //Step2 Login with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	Go to Global Setting -> Add page
            mainPage.ClickDropdownMenu(mainPage.MnGlobalSetting, "Add Page");

            //Step4	Enter Page Name field
            string randomStr = CommonMethods.RandomString();
            string pageName = "test" + randomStr;
            mainPage.TxtPageName.SendKeys(pageName);

            //Step5 Click OK button
            mainPage.BtnPopupOk.Click();

            //VP. Check "Test" page is displayed besides "Overview" page
            //New page is displayed besides "Overview" page
            string menuBarText = mainPage.MnMainBar.Text;
            string ActualStatus = menuBarText.Contains("Overview\n\r" + pageName).ToString();
            Assert.AreEqual("True",ActualStatus);
        }

        [TestMethod]
        public void TC13()
        {
            Console.WriteLine("Verify that the newly added main parent page is positioned at the location specified as set with \"Displayed After\" field of \"New Page\" form on the main page bar/\"Parent Page\" dropped down menu");
            //Step1	Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage().Open();

            //Step2 Log in specific repository with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	Go to Global Setting -> Add page
            mainPage.ClickDropdownMenu(mainPage.MnGlobalSetting, "Add Page");

            //Step4	Enter Page Name field
            string randomStr1 = CommonMethods.RandomString();
            string pageName1 = "test" + randomStr1;
            mainPage.TxtPageName.SendKeys(pageName1);

            //Step5 Click OK button
            mainPage.BtnPopupOk.Click();

            //Step6	Go to Global Setting -> Add page
            mainPage.ClickDropdownMenu(mainPage.MnGlobalSetting, "Add Page");

            //Step7	Enter Page Name field
            string randomStr2 = CommonMethods.RandomString();
            string pageName2 = "test" + randomStr2;
            mainPage.TxtPageName.SendKeys(pageName2);

            //Step8	Click on  Displayed After dropdown list

            //Step9	Select specific page
            //Step10 Click OK button
            //VP	Check "Another Test" page is positioned besides the "Test" page


            //VP. Check "Test" page is displayed besides "Overview" page
            //New page is displayed besides "Overview" page
            string menuBarText = mainPage.MnMainBar.Text;
            string ActualStatus = menuBarText.Contains("Overview\n\r" + pageName).ToString();
            Assert.AreEqual("True", ActualStatus);
        }
    }
}
