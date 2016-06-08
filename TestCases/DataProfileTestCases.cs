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
    public class DataProfileTestCases : Testbase
    {
        /// <summary>
        /// </summary>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 2:23 AM</datetime>
        [TestMethod]
        public void TC65()
        {
            Console.WriteLine("Verify that all Pre-set Data Profiles are populated correctly");
            //Step1	Navigate to Dashboard login page	
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2	Select a specific repository 	SampleRepository
            //Step3	Enter valid Username and Password	thinh.vu/(empty)
            //Step4	Click Login	
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step5	Click Administer->Data Profiles	
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.Administer, MenuList.ChildMenuEnum.DataProfiles);
            string ActualResult1 = mainPage.GetProfileDataTableCellValue("Item type", "Action Implementation By Status");
            string ActualResult2 = mainPage.GetProfileDataTableCellValue("Item type", "Test Case Execution");
            string ActualResult3 = mainPage.GetProfileDataTableCellValue("Item type", "Test Case Execution Failure Trend");
            string ActualResult4 = mainPage.GetProfileDataTableCellValue("Item type", "Test Case Execution History");
            string ActualResult5 = mainPage.GetProfileDataTableCellValue("Item type", "Test Case Execution Results");
            string ActualResult6 = mainPage.GetProfileDataTableCellValue("Item type", "Test Case Execution Trend");
            string ActualResult7 = mainPage.GetProfileDataTableCellValue("Item type", "Test Module Execution");
            string ActualResult8 = mainPage.GetProfileDataTableCellValue("Item type", "Test Module Execution Failure Trend");
            string ActualResult9 = mainPage.GetProfileDataTableCellValue("Item type", "Test Module Execution History");
            string ActualResult10 = mainPage.GetProfileDataTableCellValue("Item type", "Test Module Execution Results");
            string ActualResult11 = mainPage.GetProfileDataTableCellValue("Item type", "Test Module Execution Results Report");
            string ActualResult12 = mainPage.GetProfileDataTableCellValue("Item type", "Test Module Execution Trend");
            string ActualResult13 = mainPage.GetProfileDataTableCellValue("Item type", "Test Module Implementation By Priority");
            string ActualResult14 = mainPage.GetProfileDataTableCellValue("Item type", "Test Module Implementation By Status");
            string ActualResult15 = mainPage.GetProfileDataTableCellValue("Item type", "Test Module Status per Assigned Users");
            string ActualResult16 = mainPage.GetProfileDataTableCellValue("Item type", "Test Objective Execution");

            //VP	Check Pre-set Data Profile are populated correctly in profiles page	
            //Pre-set Data Profile are populated correctly in profiles page
            //+ Action Implementation By Status
            //+ Test Case Execution
            //+ Test Case Execution Failed Trend
            //+ Test Case Execution History
            //+ Test Case Execution Results
            //+ Test Case Execution Trend
            //+ Test Module Execution
            //+ Test Module Execution Failure Trend
            //+ Test Module Execution History
            //+ Test Module Execution Results
            //+ Test Module Execution Results Report
            //+ Test Module Execution Trend
            //+ Test Module Implementation By Priority
            //+ Test Module Implementation By Status
            //+ Test Module Status per Assigned Users
            //+ Test Objective Execution
            Assert.AreEqual("Action", ActualResult1, " failed check 1");
            Assert.AreEqual("Test Case", ActualResult2, " failed check 2");
            Assert.AreEqual("Test Case", ActualResult3, " failed check 3");
            Assert.AreEqual("Test Case", ActualResult4, " failed check 4");
            Assert.AreEqual("Test Case", ActualResult5, " failed check 5");
            Assert.AreEqual("Test Case", ActualResult6, " failed check 6");
            Assert.AreEqual("Test Module", ActualResult7, " failed check 7");
            Assert.AreEqual("Test Module", ActualResult8, " failed check 8");
            Assert.AreEqual("Test Module", ActualResult9, " failed check 9");
            Assert.AreEqual("Test Module", ActualResult10, " failed check 10");
            Assert.AreEqual("Test Module", ActualResult11, " failed check 11");
            Assert.AreEqual("Test Module", ActualResult12, " failed check 12");
            Assert.AreEqual("Test Module", ActualResult13, " failed check 13");
            Assert.AreEqual("Test Module", ActualResult14, " failed check 14");
            Assert.AreEqual("Test Module", ActualResult15, " failed check 15");
            Assert.AreEqual("Test Objective", ActualResult16, " failed check 16");

            //Post-Condition delete newly added page
            //Close TA Dashboard Main Page
        }

        /// <summary>
        /// </summary>
        /// <author>Binh Le</author>
        /// <datetime>6/7/2016 - 2:23 AM</datetime>
        [TestMethod]
        public void TC66()
        {
            Console.WriteLine("Verify that all Pre-set Data Profiles are populated correctly");
            //Step1	Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2	Select a specific repository 	SampleRepository	
            //Step3	Enter valid Username and Password	thinh.vu/(empty)	
            //Step4	Click Login		
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step5	Click Administer->Data Profiles	
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.Administer, MenuList.ChildMenuEnum.DataProfiles);
            string allText = mainPage.GetAllValueOfColumn("Action");

            //VP	Check there is no 'Delele' or 'Edit' link appears in Action section of Pre-set Data Profiles
            Assert.AreEqual(false, allText.Contains("Delete"), "Delete link is displayed on Action column");
            Assert.AreEqual(false, allText.Contains("Edit"), "Edit link is displayed on Action column");

            //Step6	Click on Pre-set Data Profile name	
            string allText2 = mainPage.GetAllValueOfColumn(1).Trim();

            //VP	Check there is no link on Pre-set Data Profile name		Click on Pre-set Data Profiles has no function
            //VP	Check there is no checkbox appears in the left of Pre-set Data Profiles		No checkbox appears in the left of Pre-set Data Profiles
            Assert.AreEqual("", allText2, "There are still some links on the left column");

            //Post-Condition delete newly added page
            //Close TA Dashboard Main Page
        }

        /// <summary>
        /// </summary>
        /// <author>Binh Le</author>
        /// <datetime>6/8/2016 - 2:02 AM</datetime>
        [TestMethod]
        public void TC67()
        {
            Console.WriteLine("Verify that Data Profiles are listed alphabetically");
            //Step1	Navigate to Dashboard login page	
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2	Select a specific repository 	SampleRepository
            //Step3	Enter valid Username and Password	thinh.vu/(empty)	
            //Step4	Click Login
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step5	Click Administer->Data Profiles	
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.Administer, MenuList.ChildMenuEnum.DataProfiles);
            bool ActualResult = mainPage.CheckDataProfileSort("Data Profile");

            //VP	Check Data Profiles are listed alphabetically
            Assert.AreEqual(true, ActualResult, "Column Data Profile is not sorted properly");

            //Post-Condition delete newly added page
            //Close TA Dashboard Main Page
        }

        /// <summary>
        /// </summary>
        /// <author>Binh Le</author>
        /// <datetime>6/8/2016 - 2:02 AM</datetime>
        [TestMethod]
        public void TC68()
        {
            Console.WriteLine("Verify that Check Boxes are only present for non-preset Data Profiles.");
            //Step1	Navigate to Dashboard login page	
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2	Select a specific repository 	SampleRepository	
            //Step3	Enter valid Username and Password	thinh.vu/(empty)	
            //Step4	Click Login		
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step5	Click Administer->Data Profiles		
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.Administer, MenuList.ChildMenuEnum.DataProfiles);

            //Step6	Create a new Data Profile
            string profileName = "Profile" + CommonMethods.RandomString();
            mainPage.AddDataProfile(profileName, "Finish");

            //Step7	Back to Data Profiles page
            bool ActualResult1 = mainPage.IsCheckBoxOnRow(profileName);
            bool ActualResult2 = mainPage.IsCheckBoxOnRow("Test Case Execution");

            //VP	Check Check Boxes are only present for non-preset Data Profiles.
            Assert.AreEqual(true, ActualResult1, "Non-Preset profile does not have the checkbox");
            Assert.AreEqual(false, ActualResult2, "Preset profile has the checkbox");


            //Post-Condition delete newly data profile
            mainPage.DeleteDataProfile(profileName);
            //Close TA Dashboard Main Page
        }

        [TestMethod]
        public void TC69()
        {
            Console.WriteLine("Verify that user is unable to proceed to next step or finish creating data profile if  \"Name *\" field is left empty");
            //Step1	Log in Dashboard	Test/test	
            LoginPage loginPage = new LoginPage(webDriver).Open();
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step2	Navigate to Data Profiles page		
            //Step3	Click on "Add New"		
            //Step4	Click on "Next Button"	
            mainPage.AddDataProfile("", "Next");
            string ActualResult1 = mainPage.GetAlertMessage();

            //VP	Check dialog message "Please input profile name" appears		dialog message "Please input profile name" appears
            Assert.AreEqual("Please input profile name.", ActualResult1, "Alert '" + ActualResult1 + "' is not correct");

            //Step5	Click on "Finish Button"
            AddDataProfilePage addDPPage = new AddDataProfilePage(webDriver);
            addDPPage.BtnFinish.Click();
            string ActualResult2 = mainPage.GetAlertMessage();

            //VP	Check dialog message "Please input profile name" appears		dialog message "Please input profile name" appears
            Assert.AreEqual("Please input profile name.", ActualResult2, "Message " + ActualResult2 + " is not correct");

            //Post-Condition: Close TA Dashboard Main Page
        }

        [TestMethod]
        public void TC70()
        {
            Console.WriteLine("Verify that special characters ' /:*?<>|\"#[ ]{}=%; 'is not allowed for input to \"Name *\" field");
            //Step1	Log in Dashboard
            LoginPage loginPage = new LoginPage(webDriver).Open();
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step2	Navigate to Data Profiles page		
            //Step3	Click on "Add New"		
            //Step4	Input special character	 /:*?<>|"#[ ]{}=%;	
            mainPage.AddDataProfile("/:*?<>|\"#[ ]{}=%;", "Next");
            string ActualResult = mainPage.GetAlertMessage();

            //VP	Check dialog message indicates invalid characters: /:*?<>|"#[ ]{}=%; is not allowed as input for name field appears		
            Assert.AreEqual("Invalid name. The name cannot contain high ASCII characters or any of the following characters: /:*?<>|\"#[]{}=%;", ActualResult, "The message is not correct");

            //Post-Condition: Close TA Dashboard Main Page
        }

        [TestMethod]
        public void TC71()
        {
            Console.WriteLine("Verify that Data Profile names are not case sensitive");
            //Precondition: Data profile name "a" is already created
            LoginPage loginPage = new LoginPage(webDriver).Open();
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            mainPage.AddDataProfile("a", "Finish");

            //Step1	Log in Dashboard
            //Step2	Navigate to Data Profiles page
            //Step3	Click on "Add New"
            //Step4	Input charater 'A' into "Name *" field
            //Step5	Click "Next" button 
            mainPage.AddDataProfile("A", "Next");
            string ActualResult = mainPage.GetAlertMessage();

            //VP	Check dialog message "Data Profile name already exists"
            Assert.AreEqual("Data profile name already exists.", ActualResult);

            //Post-Condition: Delete all testing data profile and close TA Dashboard Main Page
            mainPage.DeleteDataProfile("a");
        }

        [TestMethod]
        public void TC72()
        {
            Console.WriteLine("Verify that all data profile types are listed under \"Item Type\" dropped down menu");
            //Step1	Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2	Select a specific repository 	SampleRepository	
            //Step3	Enter valid Username and Password	thinh.vu(empty)	
            //Step4	Click Login		
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step5	Click Administer->Data Profiles		
            //Step6	Click 'Add New' link
            mainPage.AddDataProfile("", "");
            AddDataProfilePage AddDPPage = new AddDataProfilePage(webDriver);
            bool ActualResult1 = IWebElementExtension.IsItemExists(AddDPPage.CmbItemType,"Test Modules");
            bool ActualResult2 = IWebElementExtension.IsItemExists(AddDPPage.CmbItemType, "Test Cases");
            bool ActualResult3 = IWebElementExtension.IsItemExists(AddDPPage.CmbItemType, "Test Objectives");
            bool ActualResult4 = IWebElementExtension.IsItemExists(AddDPPage.CmbItemType, "Data Sets");
            bool ActualResult5 = IWebElementExtension.IsItemExists(AddDPPage.CmbItemType, "Actions");
            bool ActualResult6 = IWebElementExtension.IsItemExists(AddDPPage.CmbItemType, "Interface Entities");
            bool ActualResult7 = IWebElementExtension.IsItemExists(AddDPPage.CmbItemType, "Test Results");
            bool ActualResult8 = IWebElementExtension.IsItemExists(AddDPPage.CmbItemType, "Test Case Results");


            //VP	Check all data profile types are listed under ""Item Type"" dropped down menu in create profile page
            //Test Modules
            //Test Cases
            //Test Objectives
            //Data Sets
            //Actions
            //Interface Entities
            //Test Results
            //Test Case Results
            Assert.AreEqual(true, ActualResult1,"'Test Modules' is not displayed in the Item Type list");
            Assert.AreEqual(true, ActualResult2, "'Test Cases' is not displayed in the Item Type list");
            Assert.AreEqual(true, ActualResult3, "'Test Objectives' is not displayed in the Item Type list");
            Assert.AreEqual(true, ActualResult4, "'Data Sets' is not displayed in the Item Type list");
            Assert.AreEqual(true, ActualResult5, "'Actions' is not displayed in the Item Type list");
            Assert.AreEqual(true, ActualResult6, "'Interface Entities' is not displayed in the Item Type list");
            Assert.AreEqual(true, ActualResult7, "'Test Reuslts' is not displayed in the Item Type list");
            Assert.AreEqual(true, ActualResult8, "'Test Case Results' is not displayed in the Item Type list");

            //Post-Condition: Delete all testing data profile and close TA Dashboard Main Page
        }

        [TestMethod]
        public void TC73()
        {
            Console.WriteLine("Verify that all data profile types are listed in priority order under \"Item Type\" dropped down menu");
            //Step1	Log in Dashboard	
            LoginPage loginPage = new LoginPage(webDriver).Open();
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step2	Navigate to Data Profiles page	
            //Step3	Click on "Add New"	
            //Step4	Click on "Item Type" dropped down menu	
            mainPage.AddDataProfile("", "");
            AddDataProfilePage AddDPPage = new AddDataProfilePage(webDriver);
            SelectElement cmbItemType = new SelectElement(AddDPPage.CmbItemType);

            //VP	Check "Item Type" items are listed in priority order	
            //Test Modules>Test Cases> Test Objectives> Data Sets> Actions> Interface Entities> Test Results> Test Cases results
            Assert.AreEqual(true, cmbItemType.Options[0].Text == "Test Modules", "item 1 is " + cmbItemType.Options[0].Text);
            Assert.AreEqual(true, cmbItemType.Options[1].Text == "Test Cases", "item 2 is " + cmbItemType.Options[1].Text);
            Assert.AreEqual(true, cmbItemType.Options[2].Text == "Test Objectives", "item 3 is " + cmbItemType.Options[2].Text);
            Assert.AreEqual(true, cmbItemType.Options[3].Text == "Data Sets", "item 4 is " + cmbItemType.Options[3].Text);
            Assert.AreEqual(true, cmbItemType.Options[4].Text == "Actions", "item 5 is " + cmbItemType.Options[4].Text);
            Assert.AreEqual(true, cmbItemType.Options[5].Text == "Interface Entities", "item 6 is " + cmbItemType.Options[5].Text);
            Assert.AreEqual(true, cmbItemType.Options[6].Text == "Test Results", "item 7 is " + cmbItemType.Options[6].Text);
            Assert.AreEqual(true, cmbItemType.Options[7].Text == "Test Case Results", "item 8 is " + cmbItemType.Options[7].Text);

            //Post-Condition: Delete all testing data profile and close TA Dashboard Main Page
        }

        [TestMethod]
        public void TC74()
        {
            Console.WriteLine("Verify that appropriate \"Related Data\" items are listed correctly corresponding to the \"Item Type\" items.");
            //Step1	Navigate to Dashboard login page	
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2	Select a specific repository 	SampleRepository
            //Step3	Enter valid Username and Password	thinh.vu(empty)
            //Step4	Click Login	
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step5	Click Administer->Data Profiles	
            //Step6	Click Add new link	
            mainPage.AddDataProfile("", "");

            //Step7	Select 'Test Modules' in 'Item Type' drop down list	
            AddDataProfilePage AddDPPage = new AddDataProfilePage(webDriver);
            //AddDPPage.CmbItemType.SelectByText("Test Modules");
            AddDPPage.CmbItemType.SelectByIndex(0);

            //VP	Check 'Related Data' items listed correctly	"Related Test Results, Related Test Cases"
            Assert.AreEqual(true, AddDPPage.CmbItemType.IsItemExists("Related Test Results"), "Related Test Results is not available in Related Data");
            Assert.AreEqual(true, AddDPPage.CmbItemType.IsItemExists("Related Test Cases"), "Related Test Cases is not available in Related Data");

            //Step8	Select 'Test Cases' in 'Item Type' drop down list	
            AddDPPage.CmbItemType.SelectByText("Test Cases");

            //VP	Check 'Related Data' items listed correctly	"Related Run Results, Related Objectives"
            Assert.AreEqual(true, AddDPPage.CmbItemType.IsItemExists("Related Run Results"), "Related Run Results is not available in Related Data");
            Assert.AreEqual(true, AddDPPage.CmbItemType.IsItemExists("Related Objectives"), "Related Objectives is not available in Related Data");

            //Step9	Select 'Test Objectives' in 'Item Type' drop down list	
            AddDPPage.CmbItemType.SelectByText("Test Objectives");

            //VP	Check 'Related Data' items listed correctly	"Related Run Results, Related Test Cases"
            Assert.AreEqual(true, AddDPPage.CmbItemType.IsItemExists("Related Data"), "Related Data is not available in Related Data");

            //Step10	Select 'Data Sets' in 'Item Type' drop down list	
            AddDPPage.CmbItemType.SelectByText("Data Sets");

            //VP	Check 'Related Data' items listed correctly	No related data appears
            Assert.AreEqual(true, AddDPPage.CmbItemType.IsItemExists("Related Data"), "Related Data is not available in Related Data");

            //Step11	Select 'Actions' in 'Item Type' drop down list	
            AddDPPage.CmbItemType.SelectByText("Actions");

            //VP	Check 'Related Data' items listed correctly	Action Arguments
            Assert.AreEqual(true, AddDPPage.CmbItemType.IsItemExists("Related Data"), "Related Data is not available in Related Data");

            //Step12	Select 'Interface Entities' in 'Item Type' drop down list
            AddDPPage.CmbItemType.SelectByText("Interface Entities");

            //VP	Check 'Related Data' items listed correctly	Interface Elements
            Assert.AreEqual(true, AddDPPage.CmbItemType.IsItemExists("Related Data"), "Related Data is not available in Related Data");

            //Step13	Select 'Test Results' in 'Item Type' drop down list	
            AddDPPage.CmbItemType.SelectByText("Test Results");

            //VP	Check 'Related Data' items listed correctly	"Related Test Modules, Related Test Cases"
            Assert.AreEqual(true, AddDPPage.CmbItemType.IsItemExists("Related Test Modules"), "Related Test Modules is not available in Related Data");
            Assert.AreEqual(true, AddDPPage.CmbItemType.IsItemExists("Related Test Cases"), "Related Test Cases is not available in Related Data");

            //Step14	Select 'Test Case Results' in 'Item Type' drop down list	
            AddDPPage.CmbItemType.SelectByText("Test Cases Results");

            //VP	Check 'Related Data' items listed correctly	No related data appears
            Assert.AreEqual(true, AddDPPage.CmbItemType.IsItemExists("Related Data"), "Related Data is not available in Related Data");

            //Post-Condition: Delete all testing data profile and close TA Dashboard Main Page
        }


    }
}
