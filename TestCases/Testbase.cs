using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.Common;
using Group1Project.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


namespace Group1Project.TestCases


{
    [TestClass]
    public class Testbase
    {
        [TestInitialize]

        public void TestInitializeMethod()
        {
            Console.WriteLine("Run Test Initialize");

            //Start Firefox browser and maximize window
            Constant.WebDriver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile(), TimeSpan.FromSeconds(180));
            Constant.WebDriver.Manage().Window.Maximize();
            Constant.WebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));

            //Navigate to Railway's homepage
            Constant.WebDriver.Navigate().GoToUrl(Constant.LoginPageURL);
        }


        [TestCleanup]
        public void TestCleanupMethod()
        {

            Console.WriteLine("Run Test Cleanup");

            //Close browser
            //Constant.WebDriver.Quit();

        }


    }
}
