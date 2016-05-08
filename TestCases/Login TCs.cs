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
            LoginPage lp = new LoginPage().Open();

            //2		Enter valid username and password
            //3		Click on "Login" button
            lp.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //4		Verify that Dashboard Mainpage appears
            VP.VerifyUserShouldBeLogged(Constant.DefaultUsername);
        }

        [TestMethod]
        public void TC02()
        {
            Console.WriteLine("TC02 - Verify that user fails to login specific repository successfully via Dashboard login page with incorrect credentials");
            //1		Navigate to Dashboard login page
            LoginPage lp = new LoginPage().Open();

            //2		Enter invalid username and password
            //3		Click on "Login" button
            lp.Login(Constant.InvalidUsername, Constant.InvalidPassword, Constant.DefaultRepository);

            //4		Verify that Dashboard Error message "Username or password is invalid" appears
            //TDB;
        }


        [TestMethod]
        public void TC03()
        {
            Console.WriteLine("TC03 - Verify that user fails to log in specific repository successfully via Dashboard login page with correct username and incorrect password");
            //1		Navigate to Dashboard login page
            LoginPage lp = new LoginPage().Open();

            //2		Enter valid username and invalid password
            //3		Click on "Login" button
            lp.Login(Constant.DefaultUsername, Constant.InvalidPassword, Constant.DefaultRepository);

            //4		Verify that Dashboard Error message "Username or password is invalid" appears
            //TDB;
        }

        [TestMethod]
        public void TC04()
        {
            Console.WriteLine("TC04 - Verify that user is able to log in different repositories successfully after logging out current repository");

            //1		Navigate to Dashboard login page
            LoginPage lp = new LoginPage().Open();
            //2		Enter valid username and password of default repository
            //3		Click on "Login" button
            lp.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            //4		Click on "Logout" button

            HomePage hp = new HomePage();
            hp.Logout();

            //5		Select a different repository
            //6		Enter valid username and password of this repository

            lp.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.Repository2);
            
            //7		Verify that Dashboard Mainpage appears
            VP.VerifyUserShouldBeLogged(Constant.DefaultUsername);
        }
    }
}
