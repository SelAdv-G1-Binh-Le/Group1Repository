using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Group1Project.Common;
using Group1Project.PageObjects;

namespace Group1Project.TestCases
{
    [TestClass]
    public class LOGIN : Testbase
    {
        [TestMethod]
        public void TC01()
        {
            Console.WriteLine("TC01 - Verify that user can login specific repository successfully via Dashboard login page with correct credentials");                        
            //1		Navigate to Dashboard login page
            LoginPage lp = new LoginPage().Open();

            //2		Enter valid username and password
            //3		Click on "Login" button
            lp.Login(Constant.DefaultUsername, Constant.DefaultPassword, Constant.DefaultRepository);

            //4		Verify that Dashboard Mainpage appears
            VP.VerifyUserShouldBeLogged(Constant.DefaultUsername);
            
                    

          

         
        }
    }
}
