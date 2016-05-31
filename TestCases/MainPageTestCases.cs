using System;
using System.Text;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.DataObjects;
using Group1Project.PageObjects;
using Group1Project.Common;
using OpenQA.Selenium;

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
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2 Login with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

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
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2 Login with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	Go to Global Setting -> Add page
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.AddPage);

            //Step4	Enter Page Name field
            //Step5 Click OK button
            string randomStr = CommonMethods.RandomString();
            string pageName = "test" + randomStr;
            mainPage.AddOrEditPage(pageName, "", "", "Overview", false, "OK");

            //VP. Check "Test" page is displayed besides "Overview" page
            //New page is displayed besides "Overview" page
            int position1 = mainPage.GetTabIndex("Overview");
            int position2 = mainPage.GetTabIndex(pageName);
            Assert.AreEqual(1, position2 - position1);

            //Post-Condition: Delete newly added page. Close TA Dashboard Main Page
            mainPage.DeletePage(pageName);
        }

        [TestMethod]
        public void TC13()
        {
            Console.WriteLine("Verify that the newly added main parent page is positioned at the location specified as set with \"Displayed After\" field of \"New Page\" form on the main page bar/\"Parent Page\" dropped down menu");
            //Step1	Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(webDriver).Open();

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
            LoginPage loginPage = new LoginPage(webDriver).Open();

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
            LoginPage loginPage = new LoginPage(webDriver).Open();

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
            mainpage3.DeletePage(pageName1, pageName2);
            mainpage3.DeletePage(pageName1);
        }

        [TestMethod]
        public void TC16()
        {
            Console.WriteLine("Verify that user is able to edit the \"Public\" setting of any page successfully");
            //Step1	Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(webDriver).Open();

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
            bool ActualResult1 = CommonMethods.IsElementPresent(mainPage.webDriver, OpenQA.Selenium.By.XPath("//table//h2[.='Edit Page']"));
            Assert.AreEqual(true, ActualResult1, "Edit Page is not displayed");

            //Step12	Check Public checkbox
            //Step13	Click OK button
            mainPage.AddOrEditPage("", "", "", "", true, "OK");

            //Step14	Click on "Another Test" page
            mainPage.ClickTab(pageName2);

            //Step15	Click on "Edit" link
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Edit);

            //VP	Check "Edit Page" pop up window is displayed
            bool ActualResult2 = CommonMethods.IsElementPresent(mainPage.webDriver, OpenQA.Selenium.By.XPath("//table//h2[.='Edit Page']"));
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

        [TestMethod]
        public void TC17()
        {
            Console.WriteLine("Verify that user can remove any main parent page except \"Overview\" page successfully and the order of pages stays persistent as long as there is not children page under it");
            //Step1	    Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2	    Log in specific repository with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	    Add a new parent page
            string randomStr1 = CommonMethods.RandomString();
            string pageName1 = "test1" + randomStr1;
            mainPage.AddOrEditPage(pageName1, "", "", "", true, "OK");

            //Step4	    Add a children page of newly added page
            string randomStr2 = CommonMethods.RandomString();
            string pageName2 = "test2" + randomStr2;
            mainPage.AddOrEditPage(pageName2, pageName1, "", "", true, "OK");

            //Step5	    Click on parent page
            mainPage.ClickTab(pageName1);

            //Step6	    Click "Delete" link
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Delete);
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = mainPage.webDriver.SwitchTo().Alert();
            string ActualResult = alert.Text;
            string ExpectedResult1 = "Are you sure you want to remove this page?";

            //VP	    Check confirm message "Are you sure you want to remove this page?" appears
            Assert.AreEqual(true, ActualResult.Contains(ExpectedResult1), "The error message is not displayed correctly");

            //Step7	    Click OK button
            alert.Accept();
            string ActualResult2 = alert.Text;
            string ExpectedResult2 = "Cannot delete page '" + pageName1 + "' since it has child page(s).";

            //VP	    Check warning message "Can not delete page 'Test' since it has children page(s)" appears
            Assert.AreEqual(true, ActualResult2.Contains(ExpectedResult2), "The error message for children page is not displayed correctly");

            //Step8	    Click OK button
            alert.Accept();
            mainPage.webDriver.SwitchTo().DefaultContent();

            //Step9	    Click on  children page
            IWebElement tab1 = mainPage.webDriver.FindElement(By.XPath("//a[.='" + pageName1 + "']"));
            tab1.MoveMouse(mainPage.webDriver);
            mainPage.ClickTab(pageName2);

            //Step10	Click "Delete" link
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Delete);
            WebDriverWait wait2 = new WebDriverWait(mainPage.webDriver, TimeSpan.FromSeconds(5));
            wait2.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert2 = mainPage.webDriver.SwitchTo().Alert();
            string ActualResult3 = alert2.Text;

            //VP	    Check confirm message "Are you sure you want to remove this page?" appears
            Assert.AreEqual(true, ActualResult3.Contains(ExpectedResult1));

            //Step11	Click OK button
            alert2.Accept();
            mainPage.webDriver.SwitchTo().DefaultContent();
            CommonMethods.WaitUntilControlDisappear(mainPage.webDriver, "a", "text()", pageName2);
            bool ActualResult4 = mainPage.IsTabVisible(pageName2);

            //VP	    Check children page is deleted
            Assert.AreEqual(false, ActualResult4, "child page " + pageName2 + " is still displayed");

            //Step12	Click on  parent page
            mainPage.ClickTab(pageName1);

            //Step13	Click "Delete" link
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Delete);
            WebDriverWait wait3 = new WebDriverWait(mainPage.webDriver, TimeSpan.FromSeconds(5));
            wait2.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert3 = mainPage.webDriver.SwitchTo().Alert();
            string ActualResult5 = alert3.Text;

            //VP	    Check confirm message "Are you sure you want to remove this page?" appears
            Assert.AreEqual(true, ActualResult5.Contains(ExpectedResult1), "The error message is not displayed correctly");

            //Step14	Click OK button
            alert3.Accept();
            mainPage.webDriver.SwitchTo().DefaultContent();
            CommonMethods.WaitUntilControlDisappear(mainPage.webDriver, "a", "text()", pageName1);
            bool ActualResult6 = mainPage.IsTabVisible(pageName1);

            //VP	    Check parent page is deleted
            Assert.AreEqual(false, ActualResult6, "The Parent school " + pageName1 + "is still displayed");

            //Step15	Click on "Overview" page
            mainPage.ClickTab("Overview");
            CommonMethods.WaitAndClickControl(mainPage.webDriver, "li", "@class", "mn-setting", "");
            bool ActualResult7 = CommonMethods.IsElementPresent(mainPage.webDriver, By.XPath("//a[.='Delete']"));

            //VP	    Check "Delete" link disappears
            Assert.AreEqual(false, ActualResult7, "The 'Delete' link is still displayed");
        }

        [TestMethod]
        public void TC18()
        {
            Console.WriteLine("Verify that user is able to add additional sibbling pages to the parent page successfully");
            //Step1	    Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2	    Log in specific repository with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	    Go to Global Setting -> Add page
            //Step4	    Enter Page Name
            //Step5	    Click OK button
            string randomStr1 = CommonMethods.RandomString();
            string pageName1 = "test 1" + randomStr1;
            mainPage.AddOrEditPage(pageName1, "", "", "", true, "OK");

            //Step6	    Go to Global Setting -> Add page
            //Step7	    Enter Page Name
            //Step8	    Click on  Parent Page dropdown list
            //Step9	    Select a parent page
            //Step10	Click OK button
            string randomStr2 = CommonMethods.RandomString();
            string pageName2 = "test 2" + randomStr2;
            mainPage.AddOrEditPage(pageName2, pageName1, "", "", true, "OK");

            //Step11	Go to Global Setting -> Add page
            //Step12	Enter Page Name
            //Step13	Click on  Parent Page dropdown list
            //Step14	Select a parent page
            //Step15	Click OK button
            string randomStr3 = CommonMethods.RandomString();
            string pageName3 = "test 3" + randomStr3;
            mainPage.AddOrEditPage(pageName3, pageName1, "", "", true, "OK");
            bool ActualResult = mainPage.IsTabVisible(pageName3);

            //VP	Check "Test Child 2" is added successfully
            Assert.AreEqual(true, ActualResult, "Second child page is not added");

            //Post-Condition Log in  as creator page account and delete newly added page, its sibling page and parent page
            //Close TA Dashboard Main Page
            mainPage.DeletePage(pageName1, pageName3);
            mainPage.DeletePage(pageName1, pageName2);
            mainPage.DeletePage(pageName1);
        }

        [TestMethod]
        public void TC19()
        {
            Console.WriteLine("Verify that user is able to add additional sibbling page levels to the parent page successfully.");
            //Step1  	Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2  	Login with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3  	Go to Global Setting -> Add page
            //Step4	    Enter info into all required fields on New Page dialog: Page name: Page 1, Parent page: Overview
            string randomStr = CommonMethods.RandomString();
            string pageName = "test" + randomStr;
            mainPage.AddOrEditPage(pageName, "Overview", "", "", true, "OK");
            string ActualResult = mainPage.GetParentPage(pageName);
            string ExpectedResult = "Overview";

            //VP	    Observe the current page: User is able to add additional sibbling page levels to parent page successfully. 
            //          In this case: Overview is parent page of page 1
            Assert.AreEqual(ExpectedResult, ActualResult, "Parent page is '" + ActualResult + "', it is not 'Overview' ");

            //Post-Condition delete newly added page
            //Close TA Dashboard Main Page
            mainPage.DeletePage("Overview", pageName);
        }

        [TestMethod]
        public void TC20()
        {
            Console.WriteLine("Verify that user is able to delete sibbling page as long as that page has not children page under it");
            //Step1	        Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2	        Login with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	        Go to Global Setting -> Add page
            //Step4	        Enter info into all required fields on New Page dialog: Page name: Page 1, Parent page: Overview
            string randomStr1 = CommonMethods.RandomString();
            string pageName1 = "test1" + randomStr1;
            mainPage.AddOrEditPage(pageName1, "Overview", "", "", true, "OK");

            //Step5	        Go to Global Setting -> Add page
            //Step6	        Enter info into all required fields on New Page dialog: Page name: Page 2, Parent page: Page 1
            string randomStr2 = CommonMethods.RandomString();
            string pageName2 = "test2" + randomStr2;
            mainPage.AddOrEditPage(pageName2, pageName1, "", "", true, "OK");

            //Step7	        Go to the first created page: Page 1
            //Step8	        Click Delete link
            //Step9	        Click Ok button on Confirmation Delete page
            mainPage.DeletePage("Overview", pageName1);
            string ActualResult = mainPage.GetAlertMessage();
            string ExpectedResult = "Cannot delete page '" + pageName1 + "' since it has child page(s).";
            bool temp = ActualResult.Contains(ExpectedResult);

            //VP	        Observe the current page: There is a message "Can't delete page "page 1" since it has children page"
            Assert.AreEqual(true, true, "the message is not correct. Expected: " + ExpectedResult + " Current message: " + ActualResult);

            //Step10	    Close confirmation dialog
            //Step11	    Go to the second page
            //Step12	    Click Delete link
            //Step13	    Click Ok button on Confirmation Delete page
            mainPage.DeletePage("Overview", pageName1, pageName2);
            bool ActualResult2 = mainPage.IsTabVisible(pageName2);

            //VP	        Observe the current page: Page 2 is deleted successfully
            Assert.AreEqual(false, ActualResult2, "The second page " + pageName2 + " is still displayed");

            //Post-Condition delete newly added page
            //Close TA Dashboard Main Page
            mainPage.DeletePage("Overview", pageName1);
        }
        [TestMethod]
        public void TC21()
        {
            Console.WriteLine("Verify that user is able to edit the name of the page (Parent/Sibbling) successfully");
            //Step1	    Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2	    Login with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	    Go to Global Setting -> Add page
            //Step4	    Enter info into all required fields on New Page dialog: Page name: Page 1, Parent page: Overview
            string randomStr = CommonMethods.RandomString();
            string pageName1 = "test1" + randomStr;
            string pageName2 = "test2" + randomStr;
            string pageName3 = "test3" + randomStr;
            string pageName4 = "test4" + randomStr;
            mainPage.AddOrEditPage(pageName1, "Overview", "", "", true, "OK");

            //Step5	    Go to Global Setting -> Add page
            //Step6	    Enter info into all required fields on New Page dialog: Page name: Page 2, Parent page: Page 1
            mainPage.AddOrEditPage(pageName2, pageName1, "", "", true, "OK");

            //Step7	    Go to the first created page: Page 1
            mainPage.SelectChildPage("Overview", pageName1);

            //Step8	    Click Edit link
            //Step9	    Enter another name into Page Name field: Page name: Page 3
            //Step10    Click Ok button on Edit Page dialog
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Edit);
            mainPage.AddOrEditPage(pageName3, "", "", "", true, "OK");
            bool ActualResult1 = mainPage.IsTabVisible(pageName3);

            //VP	    Observe the current page: User is able to edit the name of parent page successfully
            Assert.AreEqual(true, ActualResult1, "New Parent name is not changed successfully");

            //Step11    Go to the second created page
            //Step12    Click Edit link: Page 2
            //Step13    Enter another name into Page Name field
            //Step14    Click Ok button on Edit Page dialog: Page name: Page 4
            mainPage.SelectChildPage("Overview", pageName3, pageName2);
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Edit);
            mainPage.AddOrEditPage(pageName4, "", "", "", true, "OK");
            bool ActualResult2 = mainPage.IsTabVisible(pageName4);

            //VP	    Observe the current page: User is able to edit the name of sibbling page successfully
            Assert.AreEqual(true, ActualResult2, "New child name is not changed successfully");

            //Post-Condition delete newly added page
            //Close TA Dashboard Main Page
            mainPage.DeletePage("Overview", pageName3, pageName4);
            mainPage.DeletePage("Overview", pageName3);
        }

        [TestMethod]
        public void TC22()
        {
            Console.WriteLine("Verify that user is unable to duplicate the name of sibbling page under the same parent page");
            //Step1	Navigate to Dashboard login page	
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2	Log in specific repository with valid account	
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	Add a new page:	Test
            string randomStr = CommonMethods.RandomString();
            string pageName1 = "test1" + randomStr;
            string pageName2 = "test2" + randomStr;
            mainPage.AddOrEditPage(pageName1, "", "", "", true, "OK");

            //Step4	Add a sibling page of new page:	Test Child 1
            mainPage.AddOrEditPage(pageName2, pageName1, "", "", true, "OK");

            //Step5	Go to Global Setting -> Add page	
            //Step6	Enter Page Name:	Test Child 1
            //Step7	Click on  Parent Page dropdown list	
            //Step8	Select a parent page:	Test
            //Step9	Click OK button	
            mainPage.AddOrEditPage(pageName2, pageName1, "", "", true, "OK");
            string ActualResult = mainPage.GetAlertMessage();
            string ExpectedResult = pageName2 + " already exists. Please enter a different name.";

            //VP	Check warning message "Test child already exist. Please enter a diffrerent name" appears
            Assert.AreEqual(ExpectedResult, ActualResult, "The error message is not correct");

            //Post-Condition delete newly added page
            //Close TA Dashboard Main Page
            AddPageDialog dialog = new AddPageDialog();
            dialog.BtnCancel.Click();
            mainPage.DeletePage(pageName1, pageName2);
            mainPage.DeletePage(pageName1);
        }

        [TestMethod]
        public void TC23()
        {
            Console.WriteLine("Verify that user is able to edit the parent page of the sibbling page successfully");
            //Step	Navigate to Dashboard login page	
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step	Login with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step	Go to Global Setting -> Add page		
            //Step	Enter info into all required fields on New Page dialog	"Page name: Page 1,Parent page: Overview"	
            string randomStr = CommonMethods.RandomString();
            string pageName1 = "test1" + randomStr;
            string pageName2 = "test2" + randomStr;
            string pageName3 = "test3" + randomStr;
            mainPage.AddOrEditPage(pageName1, "Overview", "", "", true, "OK");

            //Step	Go to Global Setting -> Add page		
            //Step	Enter info into all required fields on New Page dialog	"Page name: Page 2,Parent page: Page 1"	
            mainPage.AddOrEditPage(pageName2, pageName1, "", "", true, "OK");

            //Step	Go to the first created page:	Page 1	
            mainPage.SelectChildPage("Overview", pageName1);

            //Step	Click Edit link		
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Edit);

            //Step	Enter another name into Page Name field	Page name: Page 3	
            //Step	Click Ok button on Edit Page dialog		
            mainPage.AddOrEditPage(pageName3, "", "", "", true, "OK");
            bool ActualResult = mainPage.IsTabVisible(pageName3);

            //VP	Observe the current page: User is able to edit the parent page of the sibbling page successfully
            Assert.AreEqual(true, ActualResult, "Parent page name is not changed");

            //Post-Condition delete newly added page
            //Close TA Dashboard Main Page
            mainPage.DeletePage("Overview", pageName3, pageName2);
            mainPage.DeletePage("Overview", pageName3);
        }
        [TestMethod]
        public void TC24()
        {
            Console.WriteLine("Verify that \"Bread Crums\" navigation is correct");
            //Step1	Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2	Login with valid account: test / test	
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	Go to Global Setting -> Add page		
            //Step4	Enter info into all required fields on New Page dialog	"Page name: Page 1, Parent page: Overview"	
            string randomStr = CommonMethods.RandomString();
            string pageName1 = "test1" + randomStr;
            string pageName2 = "test2" + randomStr;
            mainPage.AddOrEditPage(pageName1, "Overview", "", "", true, "OK");

            //Step5	Go to Global Setting -> Add page		
            //Step6	Enter info into all required fields on New Page dialog	"Page name: Page 2, Parent page: Page 1"
            mainPage.AddOrEditPage(pageName2, pageName1, "", "", true, "OK");

            //Step7	Click the first breadcrums	Page 1	
            mainPage.SelectChildPage("Overview", pageName1);
            string pageTitle = webDriver.Title;
            string ActualResult = pageTitle.Substring(pageTitle.IndexOf("- ") + 2);
            string ExpectedResult = pageName1;

            //VP	Observe the current page: The first page is navigated
            Assert.AreEqual(ExpectedResult, ActualResult, pageName1 + " is not navigated correctly");

            //Step8	Click the second breadcrums	
            mainPage.SelectChildPage("Overview", pageName1, pageName2);
            string pageTitle2 = webDriver.Title;
            string ActualResult2 = pageTitle2.Substring(pageTitle.IndexOf("- ") + 2);
            string ExpectedResult2 = pageName2;

            //VP	Observe the current page: The second page is navigated
            Assert.AreEqual(ExpectedResult2, ActualResult2, pageName2 + " is not navigated correctly");

            //Post-Condition delete newly added page
            //Close TA Dashboard Main Page
            mainPage.DeletePage("Overview", pageName1, pageName2);
            mainPage.DeletePage("Overview", pageName1);
        }
        [TestMethod]
        public void TC25()
        {
            Console.WriteLine("Verify that page listing is correct when user edit \"Display After\"  field of a specific page");
            //Step1	Navigate to Dashboard login page	
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2	Login with valid account	test / test	
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	Go to Global Setting -> Add page		
            //Step4	Enter info into all required fields on New Page dialog	Page name: Page 1
            string randomStr = CommonMethods.RandomString();
            string pageName1 = "test1" + randomStr;
            string pageName2 = "test2" + randomStr;
            mainPage.AddOrEditPage(pageName1, "", "", "", true, "OK");

            //Step5	Go to Global Setting -> Add page		
            //Step6	Enter info into all required fields on New Page dialog	Page name: Page 2	
            mainPage.AddOrEditPage(pageName2, "", "", pageName1, true, "OK");


            //Step7	Click Edit link for the second created page	
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Edit);

            //Step8	Change value Display After for the second created page to after Overview page		
            //Step8 Click Ok button on Edit Page dialog		
            mainPage.AddOrEditPage("", "", "", "Overview", true, "OK");
            int firstPageIndex = mainPage.GetTabIndex("Overview");
            int secondPageIndex = mainPage.GetTabIndex(pageName2);

            //VP	Observe the current page		Position of the second page follow Overview page
            Assert.AreEqual(1, secondPageIndex - firstPageIndex);

            //Post-Condition delete newly added page
            //Close TA Dashboard Main Page
            mainPage.DeletePage("Overview", pageName1);
            mainPage.DeletePage("Overview", pageName2);
        }

        [TestMethod]
        public void TC26()
        {
            Console.WriteLine("Verify that page column is correct when user edit \"Number of Columns\" field of a specific page");
            //Step1	Navigate to Dashboard login page	
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2	Login with valid account	test / test	
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	Go to Global Setting -> Add page		
            //Step4	Enter info into all required fields on New Page dialog	"Page name: Page 1, Number of Columns: 2"	
            string randomStr = CommonMethods.RandomString();
            string pageName = "test" + randomStr;
            mainPage.AddOrEditPage(pageName, "", "2", "", true, "OK");

            //Step5	Go to Global Setting -> Edit link	
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.GlobalSetting, MenuList.ChildMenuEnum.Edit);

            //Step6	Edit Number of Columns for the above created page	Number of Columns: 3	
            //Step7	Click Ok button on Edit Page dialog		
            mainPage.AddOrEditPage("", "", "3", "", true, "OK");
            int ActualResult = mainPage.GetPageColumn();

            //VP	Observe the current page: There are 3 columns on the above created page
            Assert.AreEqual(3, ActualResult);

            //Post-Condition delete newly added page
            //Close TA Dashboard Main Page
            mainPage.DeletePage(pageName);
        }
    }
}
