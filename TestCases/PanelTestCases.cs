﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.Common;
using Group1Project.PageObjects;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace Group1Project.TestCases
{
    [TestClass]
    public class Panel : Testbase
    {
        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/2/2016 - 03:13</datetime>
        [TestMethod]
        public void TC27()
        {
            Console.WriteLine("TC27 - Verify that when \"Choose panels\" form is expanded all pre-set panels are populated and sorted correctly");

            //1	Step	Navigate to Dashboard login page	
            //2	Step	Login with valid account	test / test
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //3	Step	Go to Global Setting -> Add page
            //4	Step	Enter page name to Page Name field.	Page 1
            //5	Step	Click OK button	           

            string pagename = "Page 1";
            mainpage.ClickAddPage().AddPage(pagename);

            //6	Step	Go to Global Setting -> Create Panel
            mainpage.MnGlobalSetting.Click();
            mainpage.LnkAddPanel.Click();

            AddPanelDialog addpaneldialog = new AddPanelDialog(webDriver);
            addpaneldialog.AddChartPanelSuccess("zbox", "name");
            mainpage.BtnChoosepanel.Click();

            //7	VP	Verify that all pre-set panels are populated and sorted correctly	
            string actual = mainpage.FindElement(By.XPath("//a[contains(.,'zbox')]//preceding::a[1]"), Constant.DefaultTimeout).GetAttribute("innerHTML");
            Assert.IsTrue(String.Compare(actual, "zbox") == -1, "New item is NOT sorted correctly!!!");

            //Clean up TC 27
            PanelsPage panelspage = new PanelsPage(webDriver);
            mainpage.DeletePage("Page 1");
            panelspage.DeletePanel("zbox");
        }


        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/3/2016 - 02:34</datetime>
        [TestMethod]
        public void TC28()
        {
            Console.WriteLine("TC28 - Verify that when \"Add New Panel\" form is on focused all other control/form is disabled or locked.");

            //1	Step	Navigate to Dashboard login page
            //2	Step	Login with valid account
            //3	Step	Click Administer link
            //4	Step	Click Panel link
            //5	Step	Click Add New link
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            IWebElementExtension.MoveMouse(mainpage.LnkAdminister, webDriver);
            mainpage.LnkPanels.Click();
            PanelsPage panelspage = new PanelsPage(webDriver);
            panelspage.LnkAddNew.Click();
            AddPanelDialog addPanelDialog = new AddPanelDialog(webDriver);
            addPanelDialog.TxtDisplayName.WaitForControl(webDriver, Constant.DefaultTimeout);

            //6	Step	Try to click other controls when Add New Panel dialog is opening
            //7	VP	Observe the current page
            Assert.IsFalse(CommonMethods.Click(mainpage.MnGlobalSetting), "Control still can be clickable!!!");
        }

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/3/2016 - 08:05</datetime>
        [TestMethod]
        public void TC29()
        {
            Console.WriteLine("TC29 - Verify that user is unable to create new panel when (*) required field is not filled");
            //1	Step	Navigate to Dashboard
            //2	Step	Select specific repository
            //3	Step	Enter valid username and password
            //4	Step	Click on Login button
            //5	Step	Click on Administer/Panels link
            //6	Step	Click on "Add new" link
            //7	Step	Click on OK button
            //8	VP	Check warning message show up.
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            IWebElementExtension.MoveMouse(mainpage.LnkAdminister, webDriver);
            mainpage.LnkPanels.Click();
            PanelsPage panelspage = new PanelsPage(webDriver);
            panelspage.LnkAddNew.Click();
            AddPanelDialog addPanelDialog = new AddPanelDialog(webDriver);

            //addPanelDialog.TxtDisplayName.WaitForControl(webDriver, Constant.DefaultTimeout);
            //addPanelDialog.BtnOK.Click();
            //string alerttext = CommonMethods.CloseAlertAndGetItsText(webDriver);
            VP.CheckText("Display Name is a required field.", addPanelDialog.AddChartPanelUnsuccess());

        }
        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/3/2016 - 09:13</datetime>
        [TestMethod]
        public void TC30()
        {
            Console.WriteLine("TC30 - Verify that no special character except '@' character is allowed to be inputted into \"Display Name\" field");

            //1	Step	Navigate to Dashboard login page
            //2	Step	Login with valid account
            //3	Step	Click Administer link
            //4	Step	Click Panel link
            //5	Step	Click Add New link        
            //6	Step	Enter value into Display Name field with special characters except "@"
            //7	Step	Click Ok button
            //8	VP	Observe the current page
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            IWebElementExtension.MoveMouse(mainpage.LnkAdminister, webDriver);
            mainpage.LnkPanels.Click();
            PanelsPage panelspage = new PanelsPage(webDriver);
            panelspage.LnkAddNew.Click();
            AddPanelDialog addPanelDialog = new AddPanelDialog(webDriver);
            VP.CheckText("Invalid display name. The name cannot contain high ASCII characters or any of the following characters: /:*?<>|\"#[]{}=%;", addPanelDialog.AddChartPanelUnsuccess("Logigear#$%"));
            Console.WriteLine("Bug document here: Invalid display name. The name can't contain high ASCII characters or any of following characters: /:*?<>|\"#{[]{};");

            //9	Step	Close Warning Message box
            //10 Step	Click Add New link        
            //11 Step	Enter value into Display Name field with special character is @
            //12 VP	Observe the current page
            addPanelDialog.TxtDisplayName.Set("Logigear@");
            addPanelDialog.BtnOK.Click();
            Assert.IsTrue(panelspage.IsPanelPresent("Logigear@"), "Panel \"Logigear@\" is NOT present");

            //Clean up TC 30
            //panelspage.DeletePanel("Logigear@");
        }

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/3/2016 - 09:27</datetime>
        [TestMethod]
        public void TC31()
        {
            Console.WriteLine("TC31 - Verify that correct panel setting form is displayed with corresponding panel type selected");

            //1	Step	Navigate to Dashboard login page
            //2	Step	Login with valid account
            //3	Step	Click on Administer/Panels link
            //4	Step	Click on Add new link
            //5	VP	Verify that chart panel setting form is displayed with corresponding panel type selected

            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            mainpage.LnkAdminister.MoveMouse(webDriver);
            mainpage.LnkPanels.Click();

            PanelsPage panelspage = new PanelsPage(webDriver);
            panelspage.LnkAddNew.Click();
            AddPanelDialog addpaneldialog = new AddPanelDialog(webDriver);
            addpaneldialog.TxtDisplayName.WaitForControl(webDriver, Constant.DefaultTimeout);
            Console.WriteLine("Verify Chart panel setting form is displayed \"Chart setting\" under Display Name field");
            Assert.IsNotNull(addpaneldialog.LgdChartSettings, "Chart panel setting form is NOT displayed");

            //6	Step	Select Indicator type
            //7	VP	Verify that indicator panel setting form is displayed with corresponding panel type selected

            addpaneldialog.RadIndicatorType.Click();
            addpaneldialog.TxtDisplayName.WaitForControl(webDriver, Constant.DefaultTimeout);
            Console.WriteLine("Verify Indicator panel setting form is displayed \"Indicator setting\" under Display Name field");
            Assert.IsNotNull(addpaneldialog.LgdIndicatorSettings, "Indicator panel setting form is NOT displayed");

            //8	Step	Select Report type
            //9	VP	Verify that report panel setting form is displayed with corresponding panel type selected - Report panel setting form is displayed "View mode" under Display Name.

            addpaneldialog.RadReportType.Click();
            Console.WriteLine("Bug document here: Report panel setting form is displayed \"View mode\" under Display Name.");
        }

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/3/2016 - 10:27</datetime>
        [TestMethod]
        public void TC32()
        {
            Console.WriteLine("TC32 - Verify that user is not allowed to create panel with duplicated \"Display Name\"  ");

            //1	Step	Navigate to Dashboard login page
            //2	Step	Login with valid account
            //3	Step	Click on Administer/Panels link
            //4	Step	Click on Add new link
            //5	Step	Enter display name to "Display name" field.
            //6	Step	Click on OK button
            //7	Step	Click on Add new link again.
            //8	Step	Enter display name same with previous display name to "display name" field. 
            //9	Step	Click on OK button
            //10	VP	Check warning message show up
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            mainpage.LnkAdminister.MoveMouse(webDriver);
            mainpage.LnkPanels.Click();
            PanelsPage panelspage = new PanelsPage(webDriver);
            panelspage.LnkAddNew.Click();
            AddPanelDialog addpaneldialog = new AddPanelDialog(webDriver);
            addpaneldialog.AddChartPanelSuccess("Duplicated panel");
            panelspage.LnkAddNew.Click();
            VP.CheckText("Duplicated panel already exists. Please enter a different name.", addpaneldialog.AddChartPanelUnsuccess("Duplicated panel"));

            //Clean up TC 32
            addpaneldialog.Close();
            panelspage.DeletePanel("Duplicated panel");

        }

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/3/2016 - 10:32</datetime>
        [TestMethod]
        public void TC33()
        {
            Console.WriteLine("TC33 - Verify that \"Data Profile\" listing of \"Add New Panel\" and \"Edit Panel\" control/form are in alphabetical order");

            //1	Step	Navigate to Dashboard login page
            //2	Step	Login with valid account
            //3	Step	Click on Administer/Panels link
            //4	Step	Click on Add new link
            //5	VP	Verify that Data Profile list is in alphabetical order
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            mainpage.LnkAdminister.MoveMouse(webDriver);
            mainpage.LnkPanels.Click();
            PanelsPage panelspage = new PanelsPage(webDriver);
            panelspage.LnkAddNew.Click();
            AddPanelDialog addpaneldialog = new AddPanelDialog(webDriver);
            SelectElement CbbProfile = new SelectElement(addpaneldialog.CbbProfile);
            int count = CbbProfile.Options.Count;
            for (int i = count - 1; i >= 1; i--)
            {
                Console.WriteLine("Check Sort: " + CbbProfile.Options[i].Text + " > " + CbbProfile.Options[i - 1].Text);
                Assert.IsTrue(String.Compare(CbbProfile.Options[i].Text, CbbProfile.Options[i - 1].Text) == 1, "Items are not sorted correctly !!!");
            }

            //6	Step	Enter a display name to display name field
            //7	Step	Click on OK button
            addpaneldialog.CbbSeriesField.SelectByValue("name");
            addpaneldialog.TxtDisplayName.Set(Constant.panelTC33);
            addpaneldialog.BtnOK.Click();

            //8	Step	Click on Edit link
            //9	VP	Verify that Data Profile list is in alphabetical order
            By dynamicbtnEdit = By.XPath("//a[contains(.,'" + Constant.panelTC33 + "')]/following::a[contains(.,'Edit')][1]");
            panelspage.FindElement(dynamicbtnEdit, Constant.DefaultTimeout).Click();
            CbbProfile = new SelectElement(addpaneldialog.CbbProfile);
            count = CbbProfile.Options.Count;
            for (int i = count - 1; i >= 1; i--)
            {
                Console.WriteLine("Check Sort: " + CbbProfile.Options[i].Text + " > " + CbbProfile.Options[i - 1].Text);
                Assert.IsTrue(String.Compare(CbbProfile.Options[i].Text, CbbProfile.Options[i - 1].Text) == 1, "Items are not sorted correctly !!!");
            }

            //Clean up TC 33
            addpaneldialog.Close();
            panelspage.DeletePanel(Constant.panelTC33);
        }

        [TestMethod]
        public void TC34()
        {
            Console.WriteLine("TC34 - Verify that newly created data profiles are populated correctly under the \"Data Profile\" dropped down menu in  \"Add New Panel\" and \"Edit Panel\" control/form");

            //1	Step	Navigate to Dashboard login page		
            //2	Step	Login with valid account	
            //3	Step	Click on Administer/Data Profiles link		
            //4	Step	Click on add new link		
            //5	Step	Enter name to Name textbox	/ giang - data	
            //6	Step	Click on Finish button		
            //7	Step	Click on Administer/Panels link		
            //8	Step	Click on add new link		
            //9	VP	Verify that "giang - data" data profiles are populated correctly under the "Data Profile" dropped down menu. / giang - data data profiles are populated correctly under the "Data Profile" dropped down menu.
            //10	Step	Enter display name to Display Name textbox	/ giang - panel	
            //11	Step	Click Ok button to create a panel		
            //12	Step	Click on edit link		
            //13	VP	Verify that "giang - data" data profiles are populated correctly under the "Data Profile" dropped down menu.	/	giang - data data profiles are populated correctly under the "Data Profile" dropped down menu.

            Console.WriteLine("TBD - Wait for Binh creates DataProfilePage Actions!");
        }

        public void TC35()
        {
            Console.WriteLine("TC35 - Verify that no special character except '@' character is allowed to be inputted into \"Chart Title\" field");

            //1	Step	Navigate to Dashboard login page
            //2	Step	Login with valid account
            //3	Step	Click Administer link
            //4	Step	Click Panel link
            //5	Step	Click Add New link
            //6	Step	Enter value into Display Name field
            //        Enter value into Chart Title field with special characters except "@"
            //7	Step	Click Ok button
            //8	VP	Observe the current page
            //9	Step	Close Warning Message box
            //10	Step	Click Add New link
            //11	Step	Enter value into Display Name field
            //        Enter value into Chart Title field with special character is @
            //12	VP	Observe the current page




        }



    }
}