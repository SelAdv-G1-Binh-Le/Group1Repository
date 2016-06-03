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

        public static void CheckCurrentRepository(IWebDriver webDriver, string repository)
        {
            MainPage hp = new MainPage(webDriver);
            string actual = hp.LblRepository.GetAttribute("text");
            Console.WriteLine("Check Current Repository: " + repository);
            Assert.AreEqual("Repository: " + repository, actual);
        }

        public static void CheckControlNotExist(IWebDriver webDriver, By by)
        {     
            Assert.IsFalse(CommonMethods.IsElementPresent(webDriver , by), "Control " + by + " Exists");
        }

        public static void CheckControlExist(IWebDriver webDriver,By by)
        {
            //Assert.IsTrue(CommonMethods.IsElementPresent(by), "Control " + by + " NOT Exist");
        }

        /// <summary>
        /// Checks the text.
        /// </summary>
        /// <param name="expected">The expected.</param>
        /// <param name="actual">The actual.</param>
        /// <author>Diep Duong</author>
        /// <datetime>6/3/2016 - 08:02</datetime>
        public static void CheckText(string expected, string actual)
        {
            Console.WriteLine("Check text:");
            Console.WriteLine("Expected: " + expected);
            Console.WriteLine("Recorded: " + actual);
            Assert.AreEqual(expected, actual, "Failed to check text!");
        }

        #endregion
    }
}
