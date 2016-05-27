using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.Common;
using Group1Project.PageObjects;
using OpenQA.Selenium;

namespace Group1Project.TestCases
{
    [TestClass]
    public class LOGIN : Testbase
    {
        [TestMethod]
        public void TC01()
        {
            Console.WriteLine("TC01 - Verify that user can login specific repository successfully via Dashboard login page with correct credentials");
            //1		Navigate to Dashboard login page
            LoginPage loginpage = new LoginPage(webDriver).Open();

            //2		Enter valid username and password
            //3		Click on "Login" button
            loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //4		Verify that Dashboard Mainpage appears
            MainPage hp = new MainPage(webDriver);
            string actual = hp.LblWelcome.Text;
            Console.WriteLine("Check User Logged: " + Constant.DefaultUsername);
            Assert.AreEqual(Constant.DefaultUsername, actual);
        }

        [TestMethod]
        public void TC02()
        {
            Console.WriteLine("TC02 - Verify that user fails to login specific repository successfully via Dashboard login page with incorrect credentials");
            //1 Navigate to Dashboard login page
            LoginPage loginpage = new LoginPage(webDriver).Open();

            //2	Enter invalid username and password
            //3	Click on "Login" button
            loginpage.Login(Constant.InvalidUsername, Constant.InvalidPassword, Constant.DefaultRepository);

            //4		Verify that Dashboard Error message "Username or password is invalid" appears
            string alerttext = CommonMethods.CloseAlertAndGetItsText(loginpage.webDriver);
            VP.CheckText(Constant.LoginFailMessage1, alerttext);
        }


        [TestMethod]
        public void TC03()
        {
            Console.WriteLine("TC03 - Verify that user fails to log in specific repository successfully via Dashboard login page with correct username and incorrect password");
            //1		Navigate to Dashboard login page
            LoginPage loginpage = new LoginPage(webDriver).Open();

            //2		Enter valid username and invalid password
            //3		Click on "Login" button
            loginpage.Login(Constant.DefaultUsername, Constant.InvalidPassword, Constant.DefaultRepository);

            //4		Verify that Dashboard Error message "Username or password is invalid" appears
            string alerttext = CommonMethods.CloseAlertAndGetItsText(loginpage.webDriver);
            VP.CheckText(Constant.LoginFailMessage1, alerttext);
        }

        [TestMethod]
        public void TC04()
        {
            Console.WriteLine("TC04 - Verify that user is able to log in different repositories successfully after logging out current repository");

            //1		Navigate to Dashboard login page
            LoginPage loginpage = new LoginPage(webDriver).Open();
            //2		Enter valid username and password of default repository
            //3		Click on "Login" button
            loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            //4		Click on "Logout" button

            MainPage mainpage = new MainPage(webDriver);
            loginpage = mainpage.Logout();

            //5		Select a different repository
            //6		Enter valid username and password of this repository                        

            mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.Repository2);

            //7		Verify that Dashboard Mainpage appears
            //VP.VerifyUserShouldBeLogged(Constant.DefaultUsername);
            string actual = mainpage.LblWelcome.Text;
            Console.WriteLine("Check User Logged: " + Constant.DefaultUsername);
            Assert.AreEqual(Constant.DefaultUsername, actual);

        }

        [TestMethod]
        public void TC05()
        {
            Console.WriteLine("TC05 - Verify that there is no Login dialog when switching between 2 repositories with the same account");

            //1	Step	Navigate to Dashboard login page
            //2	Step	Login with valid account for the first repository
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            //3	Step	Choose another repository in Repository list
            mainpage.ChangeRepository(Constant.Repository2);

            //4	VP	Observe the current page - There is no Login Repository dialog
            //5	VP	Observe the current page - The Repository menu displays name of switched repository
            //VP.CheckCurrentRepository(webDriver, Constant.Repository2);

            string actual = mainpage.LblRepository.GetAttribute("text");
            Console.WriteLine("Check Current Repository: " + Constant.Repository2);
            Assert.AreEqual("Repository: " + Constant.Repository2, actual);

            string xPath = "//div[@class='form']//div[@class='ltext']";

            VP.CheckControlNotExist(webDriver, By.XPath(xPath));

        }

        [TestMethod]
        public void TC06()
        {
            Console.WriteLine("TC06 - Verify that \"Password\" input is case sensitive");

            //1	Step	Navigate to Dashboard login page		
            //2	Step	Login with the account has uppercase password	test / TEST	
            //3	VP	Observe the current page - Main page is displayed
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login("test", "TEST", Constant.DefaultRepository);
            //VP.VerifyUserShouldBeLogged("test");

            string actual = mainpage.LblWelcome.Text;
            Console.WriteLine("Check User Logged: test");
            Assert.AreEqual("test", actual);

            //4	Step	Logout TA Dashboard		
            //5	Step	Login with the above account but enter lowercase password	test / test	
            //6	VP	Observe the current page - Dashboard Error message "Username or password is invalid" appears
            mainpage.Logout().Login("test", "test", Constant.DefaultRepository);
            string alerttext = CommonMethods.CloseAlertAndGetItsText(webDriver);
            VP.CheckText(Constant.LoginFailMessage1, alerttext);
        }

        [TestMethod]
        public void TC07()
        {
            Console.WriteLine("TC07 - Verify that \"Username\" is not case sensitive");

            //1	Step	Navigate to Dashboard login page		
            //2	Step	Login with the account has uppercase username	UPPERCASEUSERNAME / uppercaseusername	
            //3	VP	Observe the current page - Main page is displayed
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login("UPPERCASEUSERNAME", "uppercaseusername", Constant.DefaultRepository);
            //VP.VerifyUserShouldBeLogged("uppercaseusername");

            string actual = mainpage.LblWelcome.Text;
            Console.WriteLine("Check User Logged: uppercaseusername");
            Assert.AreEqual("uppercaseusername", actual);

            //4	Step	Logout TA Dashboard		
            //5	Step	Login with the above account but enter lowercase username -	uppercaseusername / uppercaseusername	
            //6	VP	Observe the current page - Main page is displayed
            mainpage.Logout().Login("uppercaseusername", "uppercaseusername", Constant.DefaultRepository);
            //VP.VerifyUserShouldBeLogged("uppercaseusername");

            actual = mainpage.LblWelcome.Text;
            Console.WriteLine("Check User Logged: uppercaseusername");
            Assert.AreEqual("uppercaseusername", actual);

        }

        [TestMethod]
        public void TC08()
        {
            Console.WriteLine("TC08 - Verify that password with special characters is working correctly");

            //1	Step	Navigate to Dashboard login page		
            //2	Step	Login with account that has special characters password	specialCharsPassword / `!@^&*(+_=[{;'",./<?	
            //3	VP	Observe the current page - Main page is displayed
            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login("specialCharsPassword", "`!@^&*(+_=[{;'\",./<?", Constant.DefaultRepository);
            //VP.VerifyUserShouldBeLogged("specialCharsPassword");
            string actual = mainpage.LblWelcome.Text;
            Console.WriteLine("Check User Logged: specialCharsPassword");
            Assert.AreEqual("specialCharsPassword", actual);
        }

        [TestMethod]
        public void TC09()
        {
            Console.WriteLine("TC09 - Verify that username with special characters is working correctly");

            //1	Step	Navigate to Dashboard login page		
            //2	Step	Login with account that has special characters username 	`~!@$^&()',. / specialCharsUser	
            //3	VP	Observe the current page - Main page is displayed

            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login("`~!@$^&()',.", "specialCharsUser", Constant.DefaultRepository);
            //VP.VerifyUserShouldBeLogged("`~!@$^&()',.");
            string actual = mainpage.LblWelcome.Text;
            Console.WriteLine("Check User Logged: `~!@$^&()',.");
            Assert.AreEqual("`~!@$^&()',.", actual);
        }

        [TestMethod]
        public void TC10()
        {
            Console.WriteLine("TC10 - Verify that the page works correctly for the case when no input entered to Password and Username field");

            //1	Step	Navigate to Dashboard login page		
            //2	Step	Click Login button without entering data into Username and Password field		
            //3	VP	Observe the current page - There is a message "Please enter username"

            LoginPage loginpage = new LoginPage(webDriver).Open();
            MainPage mainpage = loginpage.Login("", "", Constant.DefaultRepository);
            string alerttext = CommonMethods.CloseAlertAndGetItsText(webDriver);
            VP.CheckText(Constant.LoginFailMessage2, alerttext);
        }


        [TestMethod]
        public void zSandbox()
        {
            Console.WriteLine("sSandbox test case");



        }
    }
}
