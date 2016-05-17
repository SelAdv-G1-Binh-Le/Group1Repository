using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.Common;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace Group1Project.TestCases
{
    [TestClass]
    public class Testbase
    {
        public static IWebDriver WebDriver;

        [TestInitialize]

        public void TestInitializeMethod()
        {
            Console.WriteLine("Run Test Initialize");

            //Start Firefox browser and maximize window
            WebDriver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile(), TimeSpan.FromSeconds(180));
            WebDriver.Manage().Window.Maximize();
            WebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));

            //Navigate to Dashboard's homepage
            WebDriver.Navigate().GoToUrl(Constant.LoginPageURL);
        }

        
        [TestCleanup]
        public void TestCleanupMethod()
        {
            Console.WriteLine("Run Test Cleanup");
            // CLose browser
            WebDriver.Quit();

        }
    }
}
