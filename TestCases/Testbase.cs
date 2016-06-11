using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.Common;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium.Remote;

namespace Group1Project.TestCases
{
    /// <summary>
    /// 
    /// </summary>
    /// <author>Diep Duong</author>
    /// <datetime>6/7/2016 - 00:45</datetime>
    [TestClass]
    public class Testbase
    {
        public IWebDriver webDriver;

        //DirectoryInfo di = Directory.CreateDirectory(Constant.DefaultLogFolder);
        //Stopwatch stopWatch = new Stopwatch();
        //FileStream ostrm;
        //StreamWriter writer;
        //TextWriter oldOut = Console.Out;
        //string filename = "TA DashBoard-" + CommonMethods.ConvertDateTimeToString(DateTime.Now) + ".log";

        /// <summary>
        /// Tests the initialize method.
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/7/2016 - 00:44</datetime>
        [TestInitialize]
        public void TestInitializeMethod()
        {
            //try
            //{
            //    ostrm = new FileStream(Constant.DefaultLogFolder + "\\" + filename, FileMode.OpenOrCreate, FileAccess.Write);
            //    writer = new StreamWriter(ostrm);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("- Cannot open " + Constant.DefaultLogFolder + " for writing");
            //    Console.WriteLine(e.Message);
            //    return;
            //}
            //Console.SetOut(writer);

            //Console.WriteLine("- Run Test Initialize");
            //stopWatch.Start();

            // --- Old Settings ----------------------------------------------------------
            //Start Firefox browser and maximize window            
            webDriver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile(), TimeSpan.FromSeconds(180));
            webDriver.Manage().Window.Maximize();
            //webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));           
            //----------------------------------------------------------

            // --- Remote Settings --------
            //DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
            //capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
            //capabilities.SetCapability(CapabilityType.Version,"42");
            //capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));

            //webDriver = new RemoteWebDriver(new Uri("http://localhost:8888/wd/hub"), capabilities, TimeSpan.FromSeconds(180));
            //webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(180)); 
            //webDriver.Manage().Window.Maximize();
            //--------------------------------------------------


        }

        /// <summary>
        /// Tests the cleanup method.
        /// </summary>
        /// <author>Diep Duong</author>
        /// <datetime>6/7/2016 - 00:44</datetime>
        [TestCleanup]
        public void TestCleanupMethod()
        {
            Console.WriteLine("- Run Test Cleanup");
            // CLose browser
            webDriver.Quit();
            //Console.WriteLine("TC finishes in {0} seconds", stopWatch.ElapsedMilliseconds / 1000);
            //stopWatch.Stop();

            ////Write log area
            //Console.SetOut(oldOut);
            //writer.Close();
            //ostrm.Close();
            //Console.WriteLine("- Please see the log file in " + Constant.DefaultLogFolder + "\\" + filename);
        }
    }
}
