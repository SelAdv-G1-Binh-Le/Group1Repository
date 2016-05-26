using Group1Project.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using Group1Project.Common;

namespace Group1Project.Common
{
    class VP
    {
        #region Verify Methods

        public static void VerifyUserShouldBeLogged(string username)
        {
            MainPage hp = new MainPage();
            string actual = hp.LblWelcome.Text;
            Console.WriteLine("Check User Logged: " + username);
            Assert.AreEqual(username, actual);
        }

        public static void VerifyUserShouldNotBeLogged(string errormessage)
        {

        }

        public static void CheckCurrentRepository(string repository)
        {
            MainPage hp = new MainPage();
            string actual = hp.LblRepository.GetAttribute("text");
            Console.WriteLine("Check Current Repository: " + repository);
            Assert.AreEqual("Repository: " + repository, actual);
        }

        public static void CheckControlNotExist(By by)
        {
            //Assert.IsFalse(CommonMethods.IsElementPresent(by), "Control " + by + " Exists");
        }

        public static void CheckControlExist(By by)
        {
            //Assert.IsTrue(CommonMethods.IsElementPresent(by), "Control " + by + " NOT Exist");
        }

        public static void CheckText(string expected, string actual)
        {
            Console.WriteLine("Check text:");
            Console.WriteLine("Expected: "+ expected);
            Console.WriteLine("Recorded: " + actual);            
            Assert.AreEqual(expected, actual,"Fail");
        }

                #endregion
    }
}
