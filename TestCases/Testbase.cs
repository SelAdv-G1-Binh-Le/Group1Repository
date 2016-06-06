using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.Common;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Diagnostics;

namespace Group1Project.TestCases
{
    [TestClass]
    public class Testbase
    {

        public IWebDriver webDriver;
        Stopwatch stopWatch = new Stopwatch();

        [TestInitialize]
        public void TestInitializeMethod()
        {
            Console.WriteLine("- Run Test Initialize");            
            stopWatch.Start();
            //Start Firefox browser and maximize window
            webDriver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile(), TimeSpan.FromSeconds(180));
            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        }

        [TestCleanup]
        public void TestCleanupMethod()
        {
            Console.WriteLine("- Run Test Cleanup");
            // CLose browser
            //webDriver.Quit();            
            Console.WriteLine("TC finishes in {0} seconds",stopWatch.ElapsedMilliseconds/1000);
            stopWatch.Stop();
        }
    }
}
