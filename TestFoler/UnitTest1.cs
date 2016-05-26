using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.DataObjects;
using Group1Project.PageObjects;
using Group1Project.Common;
using Group1Project.TestCases;

namespace Group1Project.TestFoler
{
    [TestClass]
    public class UnitTest1:Testbase
    {
        [TestMethod]
        public void TestMethod1()
        {
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2 Login with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	Go to Global Setting -> Add page
            Console.WriteLine(mainPage.GetTabIndex("Overview"));
        }
    }
}
