using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.DataObjects;
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
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.AddPage);


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
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting,MenuList.ChildMenuEnum.AddPage);

            //Step4	Enter Page Name field
            //Step5 Click OK button
            string randomStr = CommonMethods.RandomString();
            string pageName = "test" + randomStr;
            mainPage.AddOrEditPage(pageName, "", "", "Overview", false,"OK");

            //VP. Check "Test" page is displayed besides "Overview" page
            //New page is displayed besides "Overview" page
            int position1 = mainPage.GetTabIndex("Overview");
            int position2 = mainPage.GetTabIndex(pageName);
            Assert.AreEqual(1,position2-position1);

            //Post-Condition: Delete newly added page. Close TA Dashboard Main Page
            mainPage.DeletePage(pageName);
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
            //Step4	Enter Page Name field
            //Step5 Click OK button
            string randomStr1 = CommonMethods.RandomString();
            string pageName1 = "test" + randomStr1;
            mainPage.AddOrEditPage(pageName1, "", "", "", false, "OK");

            //Step6	Go to Global Setting -> Add page
            //Step7	Enter Page Name field
            //Step8	Click on  Displayed After dropdown lis
            //Step9	Select specific page
            //Step10 Click OK button
            string randomStr2 = CommonMethods.RandomString();
            string pageName2 = "test" + randomStr2;
            mainPage.AddOrEditPage(pageName2, "", "", pageName1, false, "OK");

            //VP. Page 1 is positioned besides the Page 2
            int position1 = mainPage.GetTabIndex(pageName1);
            int position2 = mainPage.GetTabIndex(pageName2);
            Assert.AreEqual(1, position2 - position1);

            //Post-Condition: Delete newly added page. Close TA Dashboard Main Page
            mainPage.DeletePage(pageName1);
            mainPage.DeletePage(pageName2);
        }

        [TestMethod]
        public void TC14()
        {
            Console.WriteLine("Verify that \"Public\" pages can be visible and accessed by all users of working repository");
            //Step1	Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage().Open();

            //Step2 Log in specific repository with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	Go to Global Setting -> Add page
            //Step4	Enter Page Name field
            //Step5	Check Public checkbox
            //Step6 Click OK button
            string randomStr = CommonMethods.RandomString();
            string pageName = "test" + randomStr;
            mainPage.AddOrEditPage(pageName, "", "", "", true, "OK");

            //Step7	Click on Log out link
            mainPage.Logout();

            //Step8	Log in with another valid account
            MainPage mainpage2 = loginPage.Login(Constant.UserName2, Constant.PassWord2, Constant.DefaultRepository);
            //VP Check newly added page is visibled
            bool ExpectedResult = mainpage2.IsTabVisible(pageName);
            Assert.AreEqual(ExpectedResult, true);

            //Post-Condition: Log in  as creator page account and delete newly added page
            mainpage2.Logout();
            MainPage mainpage3 = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Close TA Dashboard Main Page
            mainPage.DeletePage(pageName);
        }

        [TestMethod]
        public void TC15()
        {
            Console.WriteLine("Verify that non \"Public\" pages can only be accessed and visible to their creators with condition that all parent pages above it are \"Public\"");
            //Step1	Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage().Open();

            //Step2 Log in specific repository with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	Go to Global Setting -> Add page
            //Step4	Enter Page Name field
            //Step5	Check Public checkbox
            //Step6 Click OK button
            string randomStr1 = CommonMethods.RandomString();
            string pageName1 = "test" + randomStr1;
            mainPage.AddOrEditPage(pageName1, "", "", "", true, "OK");


            //Step7	Go to Global Setting -> Add page
            //Step8	Enter Page Name field
            //Step9	Click on  Select Parent dropdown list
            //Step10 Select specific page
            //Step11 Click OK button
            string randomStr2 = CommonMethods.RandomString();
            string pageName2 = "test" + randomStr2;
            mainPage.AddOrEditPage(pageName2, pageName1, "", "", false, "OK");

            //Step12 Click on Log out link
            mainPage.Logout();

            //Step13 Log in with another valid account
            MainPage mainpage2 = loginPage.Login(Constant.UserName2, Constant.PassWord2, Constant.DefaultRepository);

            //VP Check children is invisibled
            bool ActualResult = mainpage2.IsTabVisible(pageName2);
            Assert.AreEqual(false, ActualResult);

            //Post-Condition: Log in  as creator page account and delete newly added page and its parent page
            //Close TA Dashboard Main Page
            mainpage2.Logout();
            MainPage mainpage3 = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            mainpage3.DeletePage(pageName1,pageName2);
            mainpage3.DeletePage(pageName1);
        }

        [TestMethod]
        public void TC16()
        {
            Console.WriteLine("Verify that user is able to edit the \"Public\" setting of any page successfully");
            //Step1	Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage().Open();

            //Step2	Log in specific repository with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	Go to Global Setting -> Add page
            //Step4	Enter Page Name
            //Step5	Click OK button
            string randomStr1 = CommonMethods.RandomString();
            string pageName1 = "test1" + randomStr1;
            mainPage.AddOrEditPage(pageName1, "", "", "", true, "OK");

            //Step6	Go to Global Setting -> Add page
            //Step7	Enter Page Name
            //Step8	Check Public checkbox
            //Step9	Click OK button
            string randomStr2 = CommonMethods.RandomString();
            string pageName2 = "test2" + randomStr2;
            mainPage.AddOrEditPage(pageName2, "", "", "", true, "OK");

            //Step10	Click on "Test" page
            mainPage.ClickTab(pageName1);

            //Step11	Click on "Edit" link
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Edit);

            //VP	Check "Edit Page" pop up window is displayed
            bool ActualResult1 = CommonMethods.IsElementPresent(OpenQA.Selenium.By.XPath("//table//h2[.='Edit Page']"));
            Assert.AreEqual(true, ActualResult1,"Edit Page is not displayed");

            //Step12	Check Public checkbox
            //Step13	Click OK button
            mainPage.AddOrEditPage("", "", "", "", true, "OK");

            //Step14	Click on "Another Test" page
            mainPage.ClickTab(pageName2);

            //Step15	Click on "Edit" link
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Edit);

            //VP	Check "Edit Page" pop up window is displayed
            bool ActualResult2 = CommonMethods.IsElementPresent(OpenQA.Selenium.By.XPath("//table//h2[.='Edit Page']"));
            Assert.AreEqual(true, ActualResult2,"Edit Page is not displayed");

            //Step16	Uncheck Public checkbox
            //Step17	Click OK button
            mainPage.AddOrEditPage("", "", "", "", false, "OK");
            
            //Step18	Click Log out link
            mainPage.Logout();

            //Step19	Log in with another valid account
            MainPage mainpage2 = loginPage.Login(Constant.UserName2, Constant.PassWord2, Constant.DefaultRepository);
            bool ActualReuslt3 = mainpage2.IsTabVisible(pageName1);
            bool ActualReuslt4 = mainpage2.IsTabVisible(pageName2);

            //VP	Check "Test" Page is visible and can be accessed
            Assert.AreEqual(true, ActualReuslt3,pageName1 + " tab is not visible");

            //VP	Check "Another Test" page is invisible
            Assert.AreEqual(false, ActualReuslt4, pageName2 + " tab is visible");

            //Post-Condition: Log in  as creator page account and delete newly added page and its parent page
            //Close TA Dashboard Main Page
            mainpage2.Logout();
            MainPage mainpage3 = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            mainpage3.DeletePage(pageName1);
            mainpage3.DeletePage(pageName2);
        }

        [TestMethod]
        public void TC17()
        {
            Console.WriteLine("Verify that user can remove any main parent page except \Overview\ page successfully and the order of pages stays persistent as long as there is not children page under it");
            //Step1	    Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage().Open();

            //Step2	    Log in specific repository with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	    Add a new parent page
            string randomStr1 = CommonMethods.RandomString();
            string pageName1 = "test1" + randomStr1;
            mainPage.AddOrEditPage(pageName1, "", "", "", true, "OK");

            //Step4	    Add a children page of newly added page
            string randomStr2 = CommonMethods.RandomString();
            string pageName2 = "test1" + randomStr1;
            mainPage.AddOrEditPage(pageName2, pageName1, "", "", true, "OK");

            //Step5	    Click on parent page
            mainPage.ClickTab(pageName1);

            //Step6	    Click "Delete" link
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting,MenuList.ChildMenuEnum.Delete);

            //VP	    Check confirm message "Are you sure you want to remove this page?" appears


            //Step7	    Click OK button
            //VP	    Check warning message "Can not delete page 'Test' since it has children page(s)" appears
            //Step8	    Click OK button
            //Step9	    Click on  children page
            //Step10	Click "Delete" link
            //VP	    Check confirm message "Are you sure you want to remove this page?" appears
            //Step11	Click OK button
            //VP	    Check children page is deleted
            //Step12	Click on  parent page
            //Step13	Click "Delete" link
            //VP	    Check confirm message "Are you sure you want to remove this page?" appears
            //Step14	Click OK button
            //VP	    Check parent page is deleted
            //Step15	Click on "Overview" page
            //VP	    Check "Delete" link disappears

            
            
            
            
            
            
            
            
            
            
            
            
            //Step1	Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage().Open();

            //Step2	Log in specific repository with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	Go to Global Setting -> Add page
            //Step4	Enter Page Name
            //Step5	Click OK button
            string randomStr1 = CommonMethods.RandomString();
            string pageName1 = "test1" + randomStr1;
            mainPage.AddOrEditPage(pageName1, "", "", "", true, "OK");

            //Step6	Go to Global Setting -> Add page
            //Step7	Enter Page Name
            //Step8	Check Public checkbox
            //Step9	Click OK button
            string randomStr2 = CommonMethods.RandomString();
            string pageName2 = "test2" + randomStr2;
            mainPage.AddOrEditPage(pageName2, "", "", "", true, "OK");

            //Step10	Click on "Test" page
            mainPage.ClickTab(pageName1);

            //Step11	Click on "Edit" link
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Edit);

            //VP	Check "Edit Page" pop up window is displayed
            bool ActualResult1 = CommonMethods.IsElementPresent(OpenQA.Selenium.By.XPath("//table//h2[.='Edit Page']"));
            Assert.AreEqual(true, ActualResult1, "Edit Page is not displayed");

            //Step12	Check Public checkbox
            //Step13	Click OK button
            mainPage.AddOrEditPage("", "", "", "", true, "OK");

            //Step14	Click on "Another Test" page
            mainPage.ClickTab(pageName2);

            //Step15	Click on "Edit" link
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Edit);

            //VP	Check "Edit Page" pop up window is displayed
            bool ActualResult2 = CommonMethods.IsElementPresent(OpenQA.Selenium.By.XPath("//table//h2[.='Edit Page']"));
            Assert.AreEqual(true, ActualResult2, "Edit Page is not displayed");

            //Step16	Uncheck Public checkbox
            //Step17	Click OK button
            mainPage.AddOrEditPage("", "", "", "", false, "OK");

            //Step18	Click Log out link
            mainPage.Logout();

            //Step19	Log in with another valid account
            MainPage mainpage2 = loginPage.Login(Constant.UserName2, Constant.PassWord2, Constant.DefaultRepository);
            bool ActualReuslt3 = mainpage2.IsTabVisible(pageName1);
            bool ActualReuslt4 = mainpage2.IsTabVisible(pageName2);

            //VP	Check "Test" Page is visible and can be accessed
            Assert.AreEqual(true, ActualReuslt3, pageName1 + " tab is not visible");

            //VP	Check "Another Test" page is invisible
            Assert.AreEqual(false, ActualReuslt4, pageName2 + " tab is visible");

            //Post-Condition: Log in  as creator page account and delete newly added page and its parent page
            //Close TA Dashboard Main Page
            mainpage2.Logout();
            MainPage mainpage3 = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            mainpage3.DeletePage(pageName1);
            mainpage3.DeletePage(pageName2);
        }

    }
}
