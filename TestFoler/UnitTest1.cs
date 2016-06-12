using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.DataObjects;
using Group1Project.PageObjects;
using Group1Project.Common;
using Group1Project.TestCases;
using OpenQA.Selenium;

namespace Group1Project.TestFoler
{
    [TestClass]
    public class UnitTest1:Testbase
    {
        //[TestMethod]
        public void TestMethod1()
        {
            LoginPage loginPage = new LoginPage(webDriver).Open();
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);
            mainPage.DeleteAllProfilePage();
            IWebElement ABC = webDriver.FindElement(By.XPath("//a[.='Check All']"));
            ABC.Click();
            Console.WriteLine("ABC");
        }
    }
}
