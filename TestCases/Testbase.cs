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

        public IWebDriver webDriver;
                
        [TestInitialize]
                public void TestInitializeMethod()
        {
            Console.WriteLine("Run Test Initialize");                     

            //Start Firefox browser and maximize window
            webDriver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile(), TimeSpan.FromSeconds(180));
            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                      
        }
                
        [TestCleanup]
        public void TestCleanupMethod()
        {
            Console.WriteLine("Run Test Cleanup");
            // CLose browser
            webDriver.Quit();

        }
    }
}
