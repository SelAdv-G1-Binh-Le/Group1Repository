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

        public static void VerifyUserShouldBeLogged(string username)
        {
            
            HomePage hp = new HomePage();
            string actual = hp.LblWelcome.Text;
            Assert.AreEqual(username, actual);
 
        }

        public static void VerifyUserShouldNotBeLogged(string errormessage)
        {
           
        }
     



        #endregion
    }
}
