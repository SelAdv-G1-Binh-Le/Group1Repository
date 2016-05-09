using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.PageObjects;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Group1Project.TestCases
{
    [TestClass]
    public class zSandbox : Testbase
    {
        [TestMethod]
        public void zSandboxTest1()
        {
        
            LoginPage lp = new LoginPage().Open();
          //MainPage hp =  lp.Login("", "", Constant.DefaultRepository);

          //CommonMethods.DiepTest();

          
      
         
        }
    }
}
