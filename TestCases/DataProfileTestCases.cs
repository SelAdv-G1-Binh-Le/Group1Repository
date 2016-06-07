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

    }
}
