using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.PageObjects;
using Group1Project.Common;

namespace Group1Project.TestCases
{

    [TestClass]
    public class MainPageTestCases : Testbase
    {
        [TestMethod]
        public void TC11()
        {
            Console.WriteLine("Verify that user is unable open more than 1 \"New Page\" dialog");
            //Step1	Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage().Open();

            //Step2 Login with valid account
            loginPage.Login(Constant.DefaultUsername,Constant.DefaultPassword,Constant.DefaultRepository);

            //Step3	Go to Global Setting -> Add page


            //Step4	Try to go to Global Setting -> Add page again
            //VP. Observe the current page

        }
    }
}
