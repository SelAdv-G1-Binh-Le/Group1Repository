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
        [TestMethod]
        public void TestMethod1()
        {
            LoginPage loginPage = new LoginPage(webDriver).Open();

            //Step2 Login with valid account
            MainPage mainPage = loginPage.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //Step3	Go to Global Setting -> Add page
            mainPage.SelectChildMenu(MenuList.MainMenuEnum.Administer, MenuList.ChildMenuEnum.DataProfiles);
            //string keke = mainPage.ConvertBlankCharacter("Test Module Execution");
            string abc = mainPage.GetAllValueOfColumn(1);
            Console.WriteLine("String get ra duoc la: " + abc);


        }
    }
}
