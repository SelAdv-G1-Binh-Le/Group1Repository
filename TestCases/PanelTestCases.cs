using System;
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

            string pagename = "Page TC27";
            string panelname = "zbox";

            mainpage.ClickAddPage().AddPage(pagename);

            //6	Step	Go to Global Setting -> Create Panel
            mainpage.MnGlobalSetting.Click();
            mainpage.LnkAddPanel.Click();

            AddPanelDialog addpaneldialog = new AddPanelDialog(webDriver);
            addpaneldialog.AddChartPanelSuccess(panelname, "name");

            PanelConfigurationDialog panelConfigurationDialog = new PanelConfigurationDialog(webDriver);
            panelConfigurationDialog.Close();

            mainpage.SelectPage(pagename).OpenChoosePanels();

            //7	VP	Verify that all pre-set panels are populated and sorted correctly	
            string actual = mainpage.FindElement(By.XPath("//a[contains(.,'" + panelname + "')]//preceding::a[1]"), Constant.DefaultTimeout).GetAttribute("innerHTML");
            Assert.IsTrue(String.Compare(actual, panelname) == -1, "New item is NOT sorted correctly!!!");

            //- Clean up TC 27
            Console.WriteLine("- Clean up TC 27");
            PanelsPage panelspage = new PanelsPage(webDriver);
            mainpage.DeletePage(pagename);
            panelspage.DeletePanel(panelname);
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
            panelspage.LnkAddNew.Click();
            addPanelDialog.AddChartPanelSuccess("Logigear@");
            Assert.IsTrue(panelspage.IsPanelPresent("Logigear@"), "Panel \"Logigear@\" is NOT present");

            //- Clean up TC 30
            Console.WriteLine("- Clean up TC 30");
            panelspage.DeletePanel("Logigear@");
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

            //- Clean up TC 32
            Console.WriteLine("- Clean up TC 32");
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

            //- Clean up TC 33
            Console.WriteLine("- Clean up TC 33");
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

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/4/2016 - 16:37</datetime>
        [TestMethod]
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

            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            IWebElementExtension.MoveMouse(mainpage.LnkAdminister, webDriver);
            mainpage.LnkPanels.Click();
            PanelsPage panelspage = new PanelsPage(webDriver);
            panelspage.LnkAddNew.Click();
            AddPanelDialog addPanelDialog = new AddPanelDialog(webDriver);
            VP.CheckText("Invalid title name. The name cannot contain high ASCII characters or any of the following characters: /:*?<>|\"#[]{}=%;", addPanelDialog.AddChartPanelUnsuccess("Logigear", "Chart#$%"));
            Console.WriteLine("Bug document here: Invalid display name. The name can't contain high ASCII characters or any of following characters: /:*?<>|\"#{[]{};");

            //9	Step	Close Warning Message box
            //10	Step	Click Add New link
            //11	Step	Enter value into Display Name field
            //        Enter value into Chart Title field with special character is @
            //12	VP	Observe the current page
            panelspage.LnkAddNew.Click();
            addPanelDialog.AddChartPanelSuccess("Logigear@", "Chart@");
            Assert.IsTrue(panelspage.IsPanelPresent("Logigear@"), "Panel \"Logigear@\" is NOT present");

            //- Clean up TC 35
            Console.WriteLine("- Clean up TC 35");
            panelspage.DeletePanel("Logigear@");
        }

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/7/2016 - 23:43</datetime>
        [TestMethod]
        public void TC36()
        {
            Console.WriteLine("TC36 - Verify that all chart types ( Pie, Single Bar, Stacked Bar, Group Bar, Line ) are listed correctly under \"Chart Type\" dropped down menu.");
            //1	Step	Navigate to Dashboard login page
            //2	Step	Select a specific repository 
            //3	Step	Enter valid Username and Password
            //4	Step	Click 'Login' button
            //5	Step	Click 'Add Page' link
            //6	Step	Enter Page Name
            //7	Step	Click 'OK' button
            //8	Step	Click 'Choose Panels' button
            //9	Step	Click 'Create new panel' button
            //10	Step	Click 'Chart Type' drop-down menu
            //11	VP	Check that 'Chart Type' are listed 5 options: 'Pie', 'Single Bar', 'Stacked Bar', 'Group Bar' and 'Line'

            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            string pagename = "page_TC36";
            mainpage.ClickAddPage().AddPage(pagename);
            mainpage.MnGlobalSetting.Click();
            mainpage.LnkAddPanel.Click();
            AddPanelDialog addpaneldialog = new AddPanelDialog(webDriver);
            addpaneldialog.CbbChartType.Click();

            string[] expectedOptions = new string[5];
            expectedOptions[0] = "Pie";
            expectedOptions[1] = "Single Bar";
            expectedOptions[2] = "Stacked Bar";
            expectedOptions[3] = "Group Bar";
            expectedOptions[4] = "Line";

            SelectElement CbbChartType = new SelectElement(addpaneldialog.CbbChartType);
            int count = CbbChartType.Options.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                Console.WriteLine("Check options: " + CbbChartType.Options[i].Text);
                Assert.IsTrue(CbbChartType.Options[i].Text == expectedOptions[i], "Option is not correct !!!");
            }

            //- Clean up TC 36
            Console.WriteLine("- Clean up TC 36");
            PanelsPage panelspage = new PanelsPage(webDriver);
            addpaneldialog.Close();
            mainpage.DeletePage("page_TC36");
        }

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/7/2016 - 23:43</datetime>
        [TestMethod]
        public void TC37()
        {
            Console.WriteLine("TC37 - Verify that \"Category\", \"Series\" and \"Caption\" field are enabled and disabled correctly corresponding to each type of the \"Chart Type\"");
            //1	Step	Navigate to Dashboard login page
            //2	Step	Select a specific repository 
            //3	Step	Enter valid Username and Password
            //4	Step	Click 'Login' button
            //5	Step	Click 'Add Page' button
            //6	Step	Enter Page Name
            //7	Step	Click 'OK' button
            //8	Step	Click 'Choose Panels' button below the page button
            //9	Step	Click 'Create new panel' button
            //10 Step	Click 'Chart Type' drop-down menu
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            string pagename = "page_TC37";
            mainpage.ClickAddPage().AddPage(pagename);
            mainpage.MnGlobalSetting.Click();
            mainpage.LnkAddPanel.Click();
            AddPanelDialog addpaneldialog = new AddPanelDialog(webDriver);

            //11 Step	Select 'Pie' Chart Type
            //12 VP	Check that 'Category' and 'Caption' are disabled, 'Series' is enabled
            addpaneldialog.CbbChartType.SelectByText("Pie");
            Assert.IsTrue(addpaneldialog.CbbCategoryField.Enabled == false, "Category is enabled");
            Assert.IsTrue(addpaneldialog.CbbSeriesField.Enabled, "Series is disabled");
            Assert.IsTrue(addpaneldialog.TxtCategoryCaption.Enabled == false, "Category Caption is enabled");
            Assert.IsTrue(addpaneldialog.TxtSeriesCaption.Enabled == false, "Series Caption is enabled");

            //Console.WriteLine("CbbCategoryField - " + addpaneldialog.CbbCategoryField.Enabled);
            //Console.WriteLine("CbbSeriesField - " + addpaneldialog.CbbSeriesField.Enabled);
            //Console.WriteLine("TxtCategoryCaption - " + addpaneldialog.TxtCategoryCaption.Enabled);
            //Console.WriteLine("TxtSeriesCaption - " + addpaneldialog.TxtSeriesCaption.Enabled);                       

            //13 Step	Click 'Chart Type' drop-down menu
            //14 Step	Select 'Single Bar' Chart Type
            //15 VP	Check that 'Category' is disabled, 'Series' and 'Caption' are enabled
            addpaneldialog.CbbChartType.SelectByText("Single Bar");
            Assert.IsTrue(addpaneldialog.CbbCategoryField.Enabled == false, "Category is enabled");
            Assert.IsTrue(addpaneldialog.CbbSeriesField.Enabled, "Series is disabled");
            Assert.IsTrue(addpaneldialog.TxtCategoryCaption.Enabled, "Category Caption is disabled");
            Assert.IsTrue(addpaneldialog.TxtSeriesCaption.Enabled, "Series Caption is disabled");

            //16 Step	Click 'Chart Type' drop-down menu
            //17 Step	Select 'Stacked Bar' Chart Type
            //18 VP	Check that 'Category' ,'Series' and 'Caption' are enabled
            addpaneldialog.CbbChartType.SelectByText("Stacked Bar");
            Assert.IsTrue(addpaneldialog.CbbCategoryField.Enabled, "Category is disabled");
            Assert.IsTrue(addpaneldialog.CbbSeriesField.Enabled, "Series is disabled");
            Assert.IsTrue(addpaneldialog.TxtCategoryCaption.Enabled, "Category Caption is disabled");
            Assert.IsTrue(addpaneldialog.TxtSeriesCaption.Enabled, "Series Caption is disabled");

            //19 Step	Click 'Chart Type' drop-down menu
            //20 Step	Select 'Group Bar' Chart Type
            //21 VP	Check that 'Category' ,'Series' and 'Caption' are enabled
            addpaneldialog.CbbChartType.SelectByText("Group Bar");
            Assert.IsTrue(addpaneldialog.CbbCategoryField.Enabled, "Category is disabled");
            Assert.IsTrue(addpaneldialog.CbbSeriesField.Enabled, "Series is disabled");
            Assert.IsTrue(addpaneldialog.TxtCategoryCaption.Enabled, "Category Caption is disabled");
            Assert.IsTrue(addpaneldialog.TxtSeriesCaption.Enabled, "Series Caption is disabled");

            //22 Step	Click 'Chart Type' drop-down menu
            //23 Step	Select 'Line' Chart Type
            //24 VP	Check that 'Category' ,'Series' and 'Caption' are enabled
            addpaneldialog.CbbChartType.SelectByText("Line");
            Assert.IsTrue(addpaneldialog.CbbCategoryField.Enabled, "Category is disabled");
            Assert.IsTrue(addpaneldialog.CbbSeriesField.Enabled, "Series is disabled");
            Assert.IsTrue(addpaneldialog.TxtCategoryCaption.Enabled, "Category Caption is disabled");
            Assert.IsTrue(addpaneldialog.TxtSeriesCaption.Enabled, "Series Caption is disabled");

            //- Clean up TC 37
            Console.WriteLine("- Clean up TC 37");
            addpaneldialog.Close();
            mainpage.DeletePage("page_TC37");
        }

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/5/2016 - 22:17</datetime>
        [TestMethod]
        public void TC38()
        {
            Console.WriteLine("TC38 - Verify that all settings within \"Add New Panel\" and \"Edit Panel\" form stay unchanged when user switches between \"2D\" and \"3D\" radio buttons");

            //1	Step	Navigate to Dashboard login page
            //2	Step	Select a specific repository 
            //3	Step	Enter valid Username and Password
            //4	Step	Click 'Login' button
            //5	Step	Click 'Add Page' button
            //6	Step	Enter Page Name
            //7	Step	Click 'OK' button
            //8	Step	Click 'Choose Panels' button below page button
            //9	Step	Click 'Create new panel' button
            //10	Step	Click 'Chart Type' drop-down menu
            //11	Step	Select a specific Chart Type
            //12	Step	Select 'Data Profile' drop-down menu
            //13	Step	Enter 'Display Name' and 'Chart Title'
            //14	Step	Select 'Show Title' checkbox
            //15	Step	Select 'Legends' radio button
            //16	Step	Select 'Style' radio button
            //17	VP	Check that settings of 'Chart Type', 'Data Profile', 'Display Name', 'Chart Title', 'Show Title' and 'Legends' stay unchanged.
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            string pagename = "page_TC38";
            mainpage.ClickAddPage().AddPage(pagename);
            mainpage.MnGlobalSetting.Click();
            mainpage.LnkAddPanel.Click();
            AddPanelDialog addpaneldialog = new AddPanelDialog(webDriver);

            addpaneldialog.CbbChartType.SelectByValue("Stacked Bar");
            addpaneldialog.CbbProfile.SelectByText("Test Case Execution");
            addpaneldialog.TxtDisplayName.Set("hung_panel");
            addpaneldialog.CbbSeriesField.SelectByValue("name");
            addpaneldialog.TxtChartTitle.Set("hung_chart");
            addpaneldialog.ChkShowTitle.Check(true);
            addpaneldialog.RadLegendsTop.Click();
            addpaneldialog.RadChartStyle3D.Click();
            addpaneldialog.CbbCategoryField.SelectByValue("url");

            VP.CheckText("Stacked Bar", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Test Case Execution", addpaneldialog.CbbProfile.GetSelectedText());
            VP.CheckText("hung_panel", addpaneldialog.TxtDisplayName.GetAttribute("value"));
            VP.CheckText("hung_chart", addpaneldialog.TxtChartTitle.GetAttribute("value"));
            Assert.IsTrue(addpaneldialog.ChkShowTitle.Selected);
            Assert.IsTrue(addpaneldialog.RadLegendsTop.Selected);

            //18	Step	Select 'Style' radio button
            //19	VP	Check that settings of 'Chart Type', 'Data Profile', 'Display Name', 'Chart Title', 'Show Title' and 'Legends' stay unchanged.

            addpaneldialog.RadChartStyle2D.Click();

            VP.CheckText("Stacked Bar", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Test Case Execution", addpaneldialog.CbbProfile.GetSelectedText());
            VP.CheckText("hung_panel", addpaneldialog.TxtDisplayName.GetAttribute("value"));
            VP.CheckText("hung_chart", addpaneldialog.TxtChartTitle.GetAttribute("value"));
            Assert.IsTrue(addpaneldialog.ChkShowTitle.Selected);
            Assert.IsTrue(addpaneldialog.RadLegendsTop.Selected);

            //20	Step	Click OK button
            //21	Step	Select a page in drop-down menu
            //22	Step	Enter path of Folder
            //23	Step	Click OK button
            //24	Step	Click 'Edit Panel' button of panel 'hung_panel'
            //25	Step	Select 'Style' radio button
            //26	VP	Check that settings of 'Chart Type', 'Data Profile', 'Display Name', 'Chart Title', 'Show Title' and 'Legends' stay unchanged.

            addpaneldialog.BtnOK.Click();
            addpaneldialog.BtnOKPanelConfiguration.Click();
            mainpage.FindElement(By.XPath(Constant.dynamicxPathTC38), Constant.DefaultTimeout).Click();

            addpaneldialog.RadChartStyle3D.Click();

            VP.CheckText("Stacked Bar", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Test Case Execution", addpaneldialog.CbbProfile.GetSelectedText());
            VP.CheckText("hung_panel", addpaneldialog.TxtDisplayName.GetAttribute("value"));
            VP.CheckText("hung_chart", addpaneldialog.TxtChartTitle.GetAttribute("value"));
            Assert.IsTrue(addpaneldialog.ChkShowTitle.Selected);
            Assert.IsTrue(addpaneldialog.RadLegendsTop.Selected);

            //27	Step	Select 'Style' radio button
            //28	VP	Check that settings of 'Chart Type', 'Data Profile', 'Display Name', 'Chart Title', 'Show Title' and 'Legends' stay unchanged.

            addpaneldialog.RadChartStyle2D.Click();

            VP.CheckText("Stacked Bar", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Test Case Execution", addpaneldialog.CbbProfile.GetSelectedText());
            VP.CheckText("hung_panel", addpaneldialog.TxtDisplayName.GetAttribute("value"));
            VP.CheckText("hung_chart", addpaneldialog.TxtChartTitle.GetAttribute("value"));
            Assert.IsTrue(addpaneldialog.ChkShowTitle.Selected);
            Assert.IsTrue(addpaneldialog.RadLegendsTop.Selected);

            //- Clean up TC 38
            Console.WriteLine("- Clean up TC 38");
            addpaneldialog.Close();
            PanelsPage panelsPage = new PanelsPage(webDriver);
            panelsPage.DeletePanel("hung_panel");

            mainpage.DeletePage("page_TC38");
        }

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/6/2016 - 03:48</datetime>
        [TestMethod]
        public void TC39()
        {
            Console.WriteLine("TC39 - Verify that all settings within \"Add New Panel\" and \"Edit Panel\" form stay unchanged when user switches between \"Legends\" radio buttons");

            //1	Step	Navigate to Dashboard login page
            //2	Step	Login with valid account
            //3	Step	Click Administer link
            //4	Step	Click Panel link
            //5	Step	Click Add New link            

            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            mainpage.LnkAdminister.MoveMouse(webDriver);
            mainpage.LnkPanels.Click();
            PanelsPage panelspage = new PanelsPage(webDriver);
            panelspage.LnkAddNew.Click();
            AddPanelDialog addpaneldialog = new AddPanelDialog(webDriver);

            //Get default Settings values
            string chartype = addpaneldialog.CbbChartType.GetSelectedText();
            string dataprofile = addpaneldialog.CbbProfile.GetSelectedText();
            bool categoryField = addpaneldialog.CbbCategoryField.Enabled;
            bool seriesField = addpaneldialog.CbbSeriesField.Enabled;
            bool showtitle = addpaneldialog.ChkShowTitle.Selected;
            bool style2d = addpaneldialog.RadChartStyle2D.Selected;
            bool style3d = addpaneldialog.RadChartStyle3D.Selected;
            bool dataLabelsSeries = addpaneldialog.ChkDataLabelsSeries.Selected;
            bool dataLabelsCategories = addpaneldialog.ChkDataLabelsCategories.Selected;
            bool dataLabelsValue = addpaneldialog.ChkDataLabelsValue.Selected;
            bool dataLabelsPercentage = addpaneldialog.ChkDataLabelsPercentage.Selected;

            //6	Step	Click None radio button for Legend
            //7	VP	Observe the current page
            addpaneldialog.RadLegendsNone.Click();
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(dataLabelsSeries, addpaneldialog.ChkDataLabelsSeries.Selected);
            Assert.AreEqual(dataLabelsCategories, addpaneldialog.ChkDataLabelsCategories.Selected);
            Assert.AreEqual(dataLabelsValue, addpaneldialog.ChkDataLabelsValue.Selected);
            Assert.AreEqual(dataLabelsPercentage, addpaneldialog.ChkDataLabelsPercentage.Selected);

            //8	Step	Click Top radio button for Legend
            //9	VP	Observe the current page
            addpaneldialog.RadLegendsTop.Click();
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(dataLabelsSeries, addpaneldialog.ChkDataLabelsSeries.Selected);
            Assert.AreEqual(dataLabelsCategories, addpaneldialog.ChkDataLabelsCategories.Selected);
            Assert.AreEqual(dataLabelsValue, addpaneldialog.ChkDataLabelsValue.Selected);
            Assert.AreEqual(dataLabelsPercentage, addpaneldialog.ChkDataLabelsPercentage.Selected);

            //10 Step	Click Right radio button for Legend
            //11 VP	Observe the current page
            addpaneldialog.RadLegendsRight.Click();
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(dataLabelsSeries, addpaneldialog.ChkDataLabelsSeries.Selected);
            Assert.AreEqual(dataLabelsCategories, addpaneldialog.ChkDataLabelsCategories.Selected);
            Assert.AreEqual(dataLabelsValue, addpaneldialog.ChkDataLabelsValue.Selected);
            Assert.AreEqual(dataLabelsPercentage, addpaneldialog.ChkDataLabelsPercentage.Selected);

            //12 Step	Click Bottom radio button for Legend
            //13 VP	Observe the current page
            addpaneldialog.RadLegendsBottom.Click();
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(dataLabelsSeries, addpaneldialog.ChkDataLabelsSeries.Selected);
            Assert.AreEqual(dataLabelsCategories, addpaneldialog.ChkDataLabelsCategories.Selected);
            Assert.AreEqual(dataLabelsValue, addpaneldialog.ChkDataLabelsValue.Selected);
            Assert.AreEqual(dataLabelsPercentage, addpaneldialog.ChkDataLabelsPercentage.Selected);

            //14 Step	Click Left radio button for Legend
            //15 VP	Observe the current page
            addpaneldialog.RadLegendsLeft.Click();
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(dataLabelsSeries, addpaneldialog.ChkDataLabelsSeries.Selected);
            Assert.AreEqual(dataLabelsCategories, addpaneldialog.ChkDataLabelsCategories.Selected);
            Assert.AreEqual(dataLabelsValue, addpaneldialog.ChkDataLabelsValue.Selected);
            Assert.AreEqual(dataLabelsPercentage, addpaneldialog.ChkDataLabelsPercentage.Selected);

            //16 Step	Create a new panel
            //17 Step	Click Edit Panel link

            addpaneldialog.Close();
            panelspage.LnkAddNew.Click();
            addpaneldialog.AddChartPanelSuccess().ClickPanel(Constant.DefaultDisplayName);

            VP.CheckText(Constant.DefaultDisplayName, addpaneldialog.TxtDisplayName.GetAttribute("value"));
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(dataLabelsSeries, addpaneldialog.ChkDataLabelsSeries.Selected);
            Assert.AreEqual(dataLabelsCategories, addpaneldialog.ChkDataLabelsCategories.Selected);
            Assert.AreEqual(dataLabelsValue, addpaneldialog.ChkDataLabelsValue.Selected);
            Assert.AreEqual(dataLabelsPercentage, addpaneldialog.ChkDataLabelsPercentage.Selected);

            //18 Step	Click None radio button for Legend
            //19 VP	Observe the current page
            addpaneldialog.RadLegendsNone.Click();
            VP.CheckText(Constant.DefaultDisplayName, addpaneldialog.TxtDisplayName.GetAttribute("value"));
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(dataLabelsSeries, addpaneldialog.ChkDataLabelsSeries.Selected);
            Assert.AreEqual(dataLabelsCategories, addpaneldialog.ChkDataLabelsCategories.Selected);
            Assert.AreEqual(dataLabelsValue, addpaneldialog.ChkDataLabelsValue.Selected);
            Assert.AreEqual(dataLabelsPercentage, addpaneldialog.ChkDataLabelsPercentage.Selected);

            //20 Step	Click Top radio button for Legend
            //21 VP	Observe the current page
            addpaneldialog.RadLegendsTop.Click();
            VP.CheckText(Constant.DefaultDisplayName, addpaneldialog.TxtDisplayName.GetAttribute("value"));
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(dataLabelsSeries, addpaneldialog.ChkDataLabelsSeries.Selected);
            Assert.AreEqual(dataLabelsCategories, addpaneldialog.ChkDataLabelsCategories.Selected);
            Assert.AreEqual(dataLabelsValue, addpaneldialog.ChkDataLabelsValue.Selected);
            Assert.AreEqual(dataLabelsPercentage, addpaneldialog.ChkDataLabelsPercentage.Selected);
            //22 Step	Click Right radio button for Legend
            //23 VP	Observe the current page
            addpaneldialog.RadLegendsRight.Click();
            VP.CheckText(Constant.DefaultDisplayName, addpaneldialog.TxtDisplayName.GetAttribute("value"));
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(dataLabelsSeries, addpaneldialog.ChkDataLabelsSeries.Selected);
            Assert.AreEqual(dataLabelsCategories, addpaneldialog.ChkDataLabelsCategories.Selected);
            Assert.AreEqual(dataLabelsValue, addpaneldialog.ChkDataLabelsValue.Selected);
            Assert.AreEqual(dataLabelsPercentage, addpaneldialog.ChkDataLabelsPercentage.Selected);
            //24 Step	Click Bottom radio button for Legend
            //25 VP	Observe the current page
            addpaneldialog.RadLegendsBottom.Click();
            VP.CheckText(Constant.DefaultDisplayName, addpaneldialog.TxtDisplayName.GetAttribute("value"));
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(dataLabelsSeries, addpaneldialog.ChkDataLabelsSeries.Selected);
            Assert.AreEqual(dataLabelsCategories, addpaneldialog.ChkDataLabelsCategories.Selected);
            Assert.AreEqual(dataLabelsValue, addpaneldialog.ChkDataLabelsValue.Selected);
            Assert.AreEqual(dataLabelsPercentage, addpaneldialog.ChkDataLabelsPercentage.Selected);
            //26 Step	Click Left radio button for Legend
            //27 VP	Observe the current page
            addpaneldialog.RadLegendsLeft.Click();
            VP.CheckText(Constant.DefaultDisplayName, addpaneldialog.TxtDisplayName.GetAttribute("value"));
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(dataLabelsSeries, addpaneldialog.ChkDataLabelsSeries.Selected);
            Assert.AreEqual(dataLabelsCategories, addpaneldialog.ChkDataLabelsCategories.Selected);
            Assert.AreEqual(dataLabelsValue, addpaneldialog.ChkDataLabelsValue.Selected);
            Assert.AreEqual(dataLabelsPercentage, addpaneldialog.ChkDataLabelsPercentage.Selected);

            //- Clean up TC 39
            Console.WriteLine("- Clean up TC 39");
            addpaneldialog.Close();
            panelspage.DeletePanel(Constant.DefaultDisplayName);
        }

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/6/2016 - 03:48</datetime>
        [TestMethod]
        public void TC40()
        {
            Console.WriteLine("TC40 - Verify that all settings within \"Add New Panel\" and \"Edit Panel\" form stay unchanged when user switches between \"Legends\" radio buttons");
            //1	Step	Navigate to Dashboard login page		
            //2	Step	Select a specific repository 	Dashboard_STT	
            //3	Step	Enter valid Username and Password	hung.nguyen/(empty)	
            //4	Step	Click 'Login' button		
            //5	Step	Click 'Add Page' button		
            //6	Step	Enter Page Name	main_hung	
            //7	Step	Click 'OK' button		
            //8	Step	Click 'Choose Panels' button below 'main_hung' button		
            //9	Step	Click 'Create new panel' button

            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            string pagename = "page_TC40";
            mainpage.ClickAddPage().AddPage(pagename);
            mainpage.MnGlobalSetting.Click();
            mainpage.LnkAddPanel.Click();
            AddPanelDialog addpaneldialog = new AddPanelDialog(webDriver);

            //10 Step	Click 'Chart Type' drop-down menu		
            //11 Step	Select 'Pie' Chart Type		
            //12 VP	Check that 'Categories' checkbox is disabled, 'Series' checkbox, 'Value' checkbox and 'Percentage' checkbox are enabled		'Categories' checkbox is disabled, 'Series' checkbox, 'Value' checkbox and 'Percentage' checkbox are enabled
            addpaneldialog.CbbChartType.SelectByText("Pie");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsSeries.Enabled, "Series checkbox is is disabled");
            Assert.IsTrue(!addpaneldialog.ChkDataLabelsCategories.Enabled, "Categories checkbox is enabled");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsValue.Enabled, "Value checkbox is is disabled");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsPercentage.Enabled, "Percentage checkbox is is disabled");

            //13 Step	Click 'Chart Type' drop-down menu		
            //14 Step	Select 'Single Bar' Chart Type		
            //15 VP	Check that 'Categories' checkbox is disabled, 'Series' checkbox, 'Value' checkbox and 'Percentage' checkbox are enabled		'Categories' checkbox is disabled, 'Series' checkbox, 'Value' checkbox and 'Percentage' checkbox are enabled
            addpaneldialog.CbbChartType.SelectByText("Single Bar");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsSeries.Enabled, "Series checkbox is is disabled");
            Assert.IsTrue(!addpaneldialog.ChkDataLabelsCategories.Enabled, "Categories checkbox is enabled");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsValue.Enabled, "Value checkbox is is disabled");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsPercentage.Enabled, "Percentage checkbox is is disabled");

            //16 Step	Click 'Chart Type' drop-down menu		
            //17 Step	Select 'Stacked Bar' Chart Type		
            //18 VP	Check that 'Categories' checkbox, 'Series' checkbox, 'Value' checkbox and 'Percentage' checkbox are enabled		'Categories' checkbox, 'Series' checkbox, 'Value' checkbox and 'Percentage' checkbox are enabled
            addpaneldialog.CbbChartType.SelectByText("Stacked Bar");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsSeries.Enabled, "Series checkbox is is disabled");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsCategories.Enabled, "Categories checkbox is disabled");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsValue.Enabled, "Value checkbox is is disabled");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsPercentage.Enabled, "Percentage checkbox is is disabled");

            //19 Step	Click 'Chart Type' drop-down menu		
            //20 Step	Select 'Group Bar' Chart Type		
            //21 VP	Check that 'Categories' checkbox, 'Series' checkbox, 'Value' checkbox and 'Percentage' checkbox are enabled		'Categories' checkbox, 'Series' checkbox, 'Value' checkbox and 'Percentage' checkbox are enabled
            addpaneldialog.CbbChartType.SelectByText("Group Bar");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsSeries.Enabled, "Series checkbox is is disabled");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsCategories.Enabled, "Categories checkbox is disabled");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsValue.Enabled, "Value checkbox is is disabled");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsPercentage.Enabled, "Percentage checkbox is is disabled");

            //22 Step	Click 'Chart Type' drop-down menu		
            //23 Step	Select 'Line' Chart Type		
            //24 VP	Check that 'Categories' checkbox, 'Series' checkbox, 'Value' checkbox and 'Percentage' checkbox are disabled		'Categories' checkbox, 'Series' checkbox, 'Value' checkbox and 'Percentage' checkbox are disabled
            addpaneldialog.CbbChartType.SelectByText("Line");
            Assert.IsTrue(!addpaneldialog.ChkDataLabelsSeries.Enabled, "Series checkbox is is enabled");
            Assert.IsTrue(!addpaneldialog.ChkDataLabelsCategories.Enabled, "Categories checkbox is enabled");
            Assert.IsTrue(addpaneldialog.ChkDataLabelsValue.Enabled, "Value checkbox is is disabled");
            Assert.IsTrue(!addpaneldialog.ChkDataLabelsPercentage.Enabled, "Percentage checkbox is is enabled");

            //- Clean up TC 40
            Console.WriteLine("- Clean up TC 40");
            addpaneldialog.Close();
            mainpage.DeletePage(pagename);
        }

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/6/2016 - 03:48</datetime>
        [TestMethod]
        public void TC41()
        {
            Console.WriteLine("TC41 - Verify that all settings within \"Add New Panel\" and \"Edit Panel\" form stay unchanged when user switches between \"Data Labels\" check boxes buttons");
            //1	Step	Navigate to Dashboard login page		
            //2	Step	Login with valid account	test / test	
            //3	Step	Click Administer link		
            //4	Step	Click Panel link		
            //5	Step	Click Add New link
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            IWebElementExtension.MoveMouse(mainpage.LnkAdminister, webDriver);
            mainpage.LnkPanels.Click();
            PanelsPage panelspage = new PanelsPage(webDriver);
            panelspage.LnkAddNew.Click();
            AddPanelDialog addpaneldialog = new AddPanelDialog(webDriver);

            //Get default Settings values
            string chartype = addpaneldialog.CbbChartType.GetSelectedText();
            string dataprofile = addpaneldialog.CbbProfile.GetSelectedText();
            bool categoryField = addpaneldialog.CbbCategoryField.Enabled;
            bool seriesField = addpaneldialog.CbbSeriesField.Enabled;
            bool showtitle = addpaneldialog.ChkShowTitle.Selected;
            bool style2d = addpaneldialog.RadChartStyle2D.Selected;
            bool style3d = addpaneldialog.RadChartStyle3D.Selected;
            bool legendsNone = addpaneldialog.RadLegendsNone.Selected;
            bool legendsTop = addpaneldialog.RadLegendsTop.Selected;
            bool legendsRight = addpaneldialog.RadLegendsRight.Selected;
            bool legendsBottom = addpaneldialog.RadLegendsBottom.Selected;
            bool legendsLeft = addpaneldialog.RadLegendsLeft.Selected;

            //6	Step	Check Series checkbox for Data Labels		
            //7	VP	Observe the current page		All settings are unchange in Add New Panel dialog
            //8	Step	Uncheck Series checkbox

            addpaneldialog.ChkDataLabelsSeries.Check(true);
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(legendsNone, addpaneldialog.RadLegendsNone.Selected);
            Assert.AreEqual(legendsTop, addpaneldialog.RadLegendsTop.Selected);
            Assert.AreEqual(legendsRight, addpaneldialog.RadLegendsRight.Selected);
            Assert.AreEqual(legendsBottom, addpaneldialog.RadLegendsBottom.Selected);
            Assert.AreEqual(legendsLeft, addpaneldialog.RadLegendsLeft.Selected);
            addpaneldialog.ChkDataLabelsSeries.Check(false);

            //9	Step	Check Value checkbox for Data Labels		
            //10 VP	Observe the current page		All settings are unchange in Add New Panel dialog
            //11 Step	Uncheck Value checkbox		
            addpaneldialog.ChkDataLabelsValue.Check(true);
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(legendsNone, addpaneldialog.RadLegendsNone.Selected);
            Assert.AreEqual(legendsTop, addpaneldialog.RadLegendsTop.Selected);
            Assert.AreEqual(legendsRight, addpaneldialog.RadLegendsRight.Selected);
            Assert.AreEqual(legendsBottom, addpaneldialog.RadLegendsBottom.Selected);
            Assert.AreEqual(legendsLeft, addpaneldialog.RadLegendsLeft.Selected);
            addpaneldialog.ChkDataLabelsValue.Check(false);

            //12 Step	Check Percentage checbox for Data Labels		
            //13 VP	Observe the current page		All settings are unchange in Add New Panel dialog
            //14 Step	Uncheck Percentage checkbox	
            addpaneldialog.ChkDataLabelsPercentage.Check(true);
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(legendsNone, addpaneldialog.RadLegendsNone.Selected);
            Assert.AreEqual(legendsTop, addpaneldialog.RadLegendsTop.Selected);
            Assert.AreEqual(legendsRight, addpaneldialog.RadLegendsRight.Selected);
            Assert.AreEqual(legendsBottom, addpaneldialog.RadLegendsBottom.Selected);
            Assert.AreEqual(legendsLeft, addpaneldialog.RadLegendsLeft.Selected);
            addpaneldialog.ChkDataLabelsPercentage.Check(false);

            //15 Step	Create a new panel	Panel 1	
            //16 Step	Click Edit Panel link	
            addpaneldialog.Close();
            panelspage.LnkAddNew.Click();
            addpaneldialog.AddChartPanelSuccess(Constant.DefaultDisplayName).ClickPanel(Constant.DefaultDisplayName);

            //17 Step	Check Series checkbox for Data Labels		
            //18 VP	Observe the current page		All settings are unchange in Edit New Panel dialog
            //19 Step	Uncheck Series checkbox		
            addpaneldialog.ChkDataLabelsSeries.Check(true);
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(legendsNone, addpaneldialog.RadLegendsNone.Selected);
            Assert.AreEqual(legendsTop, addpaneldialog.RadLegendsTop.Selected);
            Assert.AreEqual(legendsRight, addpaneldialog.RadLegendsRight.Selected);
            Assert.AreEqual(legendsBottom, addpaneldialog.RadLegendsBottom.Selected);
            Assert.AreEqual(legendsLeft, addpaneldialog.RadLegendsLeft.Selected);
            addpaneldialog.ChkDataLabelsSeries.Check(false);

            //20 Step	Check Value checkbox for Data Labels		
            //21 VP	Observe the current page		All settings are unchange in Edit New Panel dialog
            //22 Step	Uncheck Value checkbox		
            addpaneldialog.ChkDataLabelsValue.Check(true);
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(legendsNone, addpaneldialog.RadLegendsNone.Selected);
            Assert.AreEqual(legendsTop, addpaneldialog.RadLegendsTop.Selected);
            Assert.AreEqual(legendsRight, addpaneldialog.RadLegendsRight.Selected);
            Assert.AreEqual(legendsBottom, addpaneldialog.RadLegendsBottom.Selected);
            Assert.AreEqual(legendsLeft, addpaneldialog.RadLegendsLeft.Selected);
            addpaneldialog.ChkDataLabelsValue.Check(false);

            //23 Step	Check Percentage checbox for Data Labels		
            //24 VP	Observe the current page		All settings are unchange in Edit New Panel dialog
            addpaneldialog.ChkDataLabelsPercentage.Check(true);
            VP.CheckText("Pie", addpaneldialog.CbbChartType.GetSelectedText());
            VP.CheckText("Action Implementation By Status", addpaneldialog.CbbProfile.GetSelectedText());
            Assert.AreEqual(categoryField, addpaneldialog.CbbCategoryField.Enabled);
            Assert.AreEqual(seriesField, addpaneldialog.CbbSeriesField.Enabled);
            Assert.AreEqual(showtitle, addpaneldialog.ChkShowTitle.Selected);
            Assert.AreEqual(style2d, addpaneldialog.RadChartStyle2D.Selected);
            Assert.AreEqual(style3d, addpaneldialog.RadChartStyle3D.Selected);
            Assert.AreEqual(legendsNone, addpaneldialog.RadLegendsNone.Selected);
            Assert.AreEqual(legendsTop, addpaneldialog.RadLegendsTop.Selected);
            Assert.AreEqual(legendsRight, addpaneldialog.RadLegendsRight.Selected);
            Assert.AreEqual(legendsBottom, addpaneldialog.RadLegendsBottom.Selected);
            Assert.AreEqual(legendsLeft, addpaneldialog.RadLegendsLeft.Selected);
            addpaneldialog.ChkDataLabelsPercentage.Check(false);

            //- Clean up TC 41
            Console.WriteLine("- Clean up TC 41");
            addpaneldialog.Close();
            panelspage.DeletePanel(Constant.DefaultDisplayName);

        }

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/6/2016 - 06:27</datetime>
        [TestMethod]
        public void TC42()
        {
            Console.WriteLine("TC42 - Verify that all pages are listed correctly under the \"Select page *\" dropped down menu of \"Panel Configuration\" form/ control");

            //1	Step	Navigate to Dashboard login page		
            //2	Step	Select a specific repository 	Dashboard_STT	
            //3	Step	Enter valid Username and Password	hung.nguyen/(empty)	
            //4	Step	Click 'Login' button		
            //5	Step	Click 'Add Page' button		
            //6	Step	Enter Page Name	main_hung1	
            //7	Step	Click 'OK' button		
            //8	Step	Click 'Add Page' button		
            //9	Step	Enter Page Name	main_hung2	
            //10 Step	Click 'OK' button		
            //11 Step	Click 'Add Page' button		
            //12 Step	Enter Page Name	main_hung3	
            //13 Step	Click 'OK' button		
            //14 Step	Click 'Choose panels' button		
            //15 Step	Click on any Chart panel instance		
            //16 Step	Click 'Select Page*' drop-down menu		
            //17 VP	Check that 'Select Page*' drop-down menu contains 3 items: 'main_hung1', 'main_hung2' and 'main_hung3'		'Select Page*' drop-down menu contains 3 items: 'main_hung1', 'main_hung2' and 'main_hung3'

            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            mainpage.AddPage("page_TC42_1");
            mainpage.AddPage("page_TC42_2");
            mainpage.AddPage("page_TC42_3");

            mainpage.OpenChoosePanels();
            PanelConfigurationDialog panelConfigurationDlg = new PanelConfigurationDialog(webDriver);

            IWebElement table = mainpage.FindElement(By.XPath("//div[@class='ptit pchart']/parent::div//table"), Constant.DefaultTimeout);
            Random r = new Random();
            int col = r.Next(1, table.GetTableColumns() + 1);
            int row = r.Next(1, table.GetTableRows());
            string dynamicxPath = "//div[@class='ptit pchart']/parent::div//table//tr[" + row + "]//td[" + col + "]//a";
            mainpage.FindElement(By.XPath(dynamicxPath), 10).Click();
            mainpage.CbbPages.Click();

            Assert.IsTrue(IWebElementExtension.IsItemExists(mainpage.CbbPages, "page_TC42_1"), "page_TC42_1 is NOT existed");
            Assert.IsTrue(IWebElementExtension.IsItemExists(mainpage.CbbPages, "page_TC42_2"), "page_TC42_2 is NOT existed");
            Assert.IsTrue(IWebElementExtension.IsItemExists(mainpage.CbbPages, "page_TC42_3"), "page_TC42_3 is NOT existed");

            //- Clean up TC 42
            Console.WriteLine("- Clean up TC 42");
            panelConfigurationDlg.Close();
            mainpage.DeletePage("page_TC42_1");
            mainpage.DeletePage("page_TC42_2");
            mainpage.DeletePage("page_TC42_3");
        }

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/7/2016 - 00:58</datetime>
        [TestMethod]
        public void TC43()
        {
            Console.WriteLine("TC43 - Verify that only integer number inputs from 300-800 are valid for \"Height *\" field");

            //1	Step	Navigate to Dashboard login page		
            //2	Step	Select a specific repository 		
            //3	Step	Enter valid Username and Password		
            //4	Step	Click 'Login' button		
            //5	Step	Click 'Add Page' button		
            //6	Step	Enter Page Name	main_hung	
            //7	Step	Click 'OK' button		
            //11 Step	Click 'Choose panels' button		
            //12 Step	Click on any Chart panel instance

            string pagename = "page_TC43";

            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            mainpage.AddPage(pagename).OpenChoosePanels();

            PanelConfigurationDialog panelConfigurationDlg = new PanelConfigurationDialog(webDriver);
            IWebElement table = mainpage.FindElement(By.XPath("//div[@class='ptit pchart']/parent::div//table"), Constant.DefaultTimeout);
            Random r = new Random();

            //13 Step	Enter integer number to 'Height *' field	299	
            //14 Step	Click OK button		
            //15 VP	Check that error message 'Panel height must be greater than or equal to 300 and lower than or equal to 800' display		Error message 'Panel height must be greater than or equal to 300 and lower than or equal to 800' display
            int col = r.Next(1, table.GetTableColumns() + 1);
            int row = r.Next(1, table.GetTableRows());
            string dynamicxPath = "//div[@class='ptit pchart']/parent::div//table//tr[" + row + "]//td[" + col + "]//a";
            mainpage.FindElement(By.XPath(dynamicxPath), 10).Click();
            VP.CheckText("Panel height must be greater than or equal to 300 and less than or equal to 800.", panelConfigurationDlg.EditPanelUnsuccess(pagename, "299"));
            Console.WriteLine("Bug document here: //15 VP - Check that error message 'Panel height must be greater than or equal to 300 and lower than or equal to 800' display");

            //16 Step	Click OK button		
            //17 Step	Enter integer number to 'Height *' field	801	
            //18 Step	Click OK button		
            //19 VP	Check that error message 'Panel height must be greater than or equal to 300 and lower than or equal to 800' display		Error message 'Panel height must be greater than or equal to 300 and lower than or equal to 800' display
            col = r.Next(1, table.GetTableColumns() + 1);
            row = r.Next(1, table.GetTableRows());
            dynamicxPath = "//div[@class='ptit pchart']/parent::div//table//tr[" + row + "]//td[" + col + "]//a";
            mainpage.FindElement(By.XPath(dynamicxPath), 10).Click();
            VP.CheckText("Panel height must be greater than or equal to 300 and less than or equal to 800.", panelConfigurationDlg.EditPanelUnsuccess(pagename, "801"));
            Console.WriteLine("Bug document here: //19 VP - Check that error message 'Panel height must be greater than or equal to 300 and lower than or equal to 800' display");

            //20 Step	Click OK button		
            //21 Step	Enter integer number to 'Height *' field	-2	
            //23 Step	Click OK button		
            //24 VP	Check that error message 'Panel height must be greater than or equal to 300 and lower than or equal to 800' display		Error message 'Panel height must be greater than or equal to 300 and lower than or equal to 800' display
            col = r.Next(1, table.GetTableColumns() + 1);
            row = r.Next(1, table.GetTableRows());
            dynamicxPath = "//div[@class='ptit pchart']/parent::div//table//tr[" + row + "]//td[" + col + "]//a";
            mainpage.FindElement(By.XPath(dynamicxPath), 10).Click();
            VP.CheckText("Panel height must be greater than or equal to 300 and less than or equal to 800.", panelConfigurationDlg.EditPanelUnsuccess(pagename, "-2"));
            Console.WriteLine("Bug document here: //24 VP - Check that error message 'Panel height must be greater than or equal to 300 and lower than or equal to 800' display");

            //25 Step	Click OK button		
            //26 Step	Enter integer number to 'Height *' field	3.1	
            //27 Step	Click OK button		
            //28 VP	Check that error message 'Panel height must be an integer number' display		Error message 'Panel height must be an integer number' display
            col = r.Next(1, table.GetTableColumns() + 1);
            row = r.Next(1, table.GetTableRows());
            dynamicxPath = "//div[@class='ptit pchart']/parent::div//table//tr[" + row + "]//td[" + col + "]//a";
            mainpage.FindElement(By.XPath(dynamicxPath), 10).Click();
            VP.CheckText("Panel height must be greater than or equal to 300 and less than or equal to 800.", panelConfigurationDlg.EditPanelUnsuccess(pagename, "3.1"));
            Console.WriteLine("Bug document here: //28 VP - Check that error message 'Panel height must be an integer number' display		Error message 'Panel height must be an integer number' display");

            //29 Step	Click OK button		
            //30 Step	Enter integer number to 'Height *' field	abc	
            //31 Step	Click OK button		
            //32 VP	Check that error message 'Panel height must be an integer number' display		Error message 'Panel height must be an integer number' display
            col = r.Next(1, table.GetTableColumns() + 1);
            row = r.Next(1, table.GetTableRows());
            dynamicxPath = "//div[@class='ptit pchart']/parent::div//table//tr[" + row + "]//td[" + col + "]//a";
            mainpage.FindElement(By.XPath(dynamicxPath), 10).Click();
            VP.CheckText("Panel height must be an integer number", panelConfigurationDlg.EditPanelUnsuccess(pagename, "abc"));

            //- Clean up TC 43
            Console.WriteLine("- Clean up TC 43");
            mainpage.DeletePage(pagename);
        }

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/7/2016 - 03:13</datetime>
        [TestMethod]
        public void TC44()
        {
            Console.WriteLine("TC44 - Verify that \"Height *\" field is not allowed to be empty");

            //1	Step	Navigate to Dashboard login page
            //2	Step	Select a specific repository 
            //3	Step	Enter valid Username and Password
            //4	Step	Click 'Login' button
            //5	Step	Click 'Add Page' button
            //6	Step	Enter Page Name
            //7	Step	Click 'OK' button
            //11 Step	Click 'Choose panels' button
            //12 Step	Click on any Chart panel instance
            //13 Step	Leave 'Height *' field empty
            //14 Step	Click OK button
            //15 VP	Check that 'Panel height is required field' message display

            string pagename = "page_TC44";
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            mainpage.AddPage(pagename).OpenChoosePanels();
            PanelConfigurationDialog panelConfigurationDlg = new PanelConfigurationDialog(webDriver);
            IWebElement table = mainpage.FindElement(By.XPath("//div[@class='ptit pchart']/parent::div//table"), Constant.DefaultTimeout);
            Random r = new Random();
            int col = r.Next(1, table.GetTableColumns() + 1);
            int row = r.Next(1, table.GetTableRows());
            string dynamicxPath = "//div[@class='ptit pchart']/parent::div//table//tr[" + row + "]//td[" + col + "]//a";
            mainpage.FindElement(By.XPath(dynamicxPath), 10).Click();
            VP.CheckText("Panel height is a required field.", panelConfigurationDlg.EditPanelUnsuccess(pagename));
            Console.WriteLine("Bug document here: //15 VP - Check that 'Panel height is required field' message display");

            //- Clean up TC 44
            Console.WriteLine("- Clean up TC 44");
            mainpage.DeletePage(pagename);
        }

        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/7/2016 - 04:01</datetime>
        [TestMethod]
        public void TC45()
        {
            Console.WriteLine("TC45 - Verify that \"Folder\" field is not allowed to be empty");

            //1	Step	Navigate to Dashboard login page
            //2	Step	Login with valid account
            //3	Step	Create a new page
            //4	Step	Click Choose Panel button
            //5	Step	Click Create New Panel button
            //6	Step	Enter all required fields on Add New Panel page
            //7	Step	Click Ok button
            //8	Step	Leave empty on Folder field
            //9	Step	Click Ok button on Panel Configuration dialog
            //10 VP	Observe the current page
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            mainpage.AddPage("Page TC45").OpenChoosePanels().BtnCreateNewPanel.Click();
            AddPanelDialog addPanelDialog = new AddPanelDialog(webDriver);
            addPanelDialog.AddChartPanelSuccess("Panel 1");
            PanelConfigurationDialog panelConfigurationDialog = new PanelConfigurationDialog(webDriver);
            VP.CheckText("Panel folder is incorrect", panelConfigurationDialog.EditPanelUnsuccess("Page TC45", "456"));

            //- Clean up TC 45
            Console.WriteLine("- Clean up TC 45");
            mainpage.DeletePage("Page TC45");
            PanelsPage panelsPage = new PanelsPage(webDriver);
            panelsPage.DeletePanel("Panel 1");
        }
        /// <summary>
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/8/2016 - 03:13</datetime>
        [TestMethod]
        public void TC46()
        {
            Console.WriteLine("TC46 - Verify that only valid folder path of corresponding item type ( e.g. Actions, Test Modules) are allowed to be entered into \"Folder\" field");
            //1	Step	Navigate to Dashboard login page		
            //2	Step	Login with valid account		
            //3	Step	Create a new page	- Page TC46	
            //4	Step	Click Choose Panel button		
            //5	Step	Click Create New Panel button

            string pagename = "Page TC46";
            string panelname = "Panel TC46";

            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            mainpage.AddPage(pagename).OpenChoosePanels().BtnCreateNewPanel.Click();
            AddPanelDialog addPanelDialog = new AddPanelDialog(webDriver);

            //6	Step	Enter all required fields on Add New Panel page	Display Name: - Panel TC46	
            //7	Step	Click Ok button
            addPanelDialog.AddChartPanelSuccess(panelname);

            //8	Step	Enter invalid folder path	Folder: abc	
            //9	Step	Click Ok button on Panel Configuration dialog		
            //10 VP	Observe the current page		There is message "Panel folder is incorrect"
            PanelConfigurationDialog panelConfigurationDialog = new PanelConfigurationDialog(webDriver);
            VP.CheckText("Panel folder is incorrect", panelConfigurationDialog.EditPanelUnsuccess(pagename, "456", "abc"));

            //11 Step	Enter valid folder path	Folder: [Repository name]/Actions	
            //12 Step	Click Ok button on Panel Configuration dialog		
            //13 VP	Observe the current page		The new panel is created
            mainpage.SelectPage(pagename).ChoosePanel(panelname);
            panelConfigurationDialog.EditPanelSuccess(pagename, "789", "/Car Rental - Mobile/Actions/Car");
            Assert.IsNotNull(mainpage.FindElement(By.XPath("//div[@class='al_lft'][@title='" + panelname + "']")), panelname + " is not existed!!!");

        }
    }


}