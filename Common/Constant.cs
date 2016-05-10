using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace Group1Project.Common
{
    public class Constant
    {
         //Diep Logigear: http://192.168.170.37:54001/TADashboard/login.jsp
         //Diep Home: http://192.168.1.2/TADashboard/login.jsp
        //Binh Home: http://192.168.1.2/TADashboard/login.jsp
        //Global home page: http://groupba.dyndns.org:54000/TADashboard/login.jsp

        public static IWebDriver WebDriver;
        public const string LoginPageURL = "http://groupba.dyndns.org:54000/TADashboard/login.jsp";
        public const string DefaultUsername = "administrator";
        public const string DefaultPassword = "";
        public const string InvalidUsername = "abc";
        public const string InvalidPassword = "abc";
        public const string DefaultRepository = "SampleRepository";
        public const string Repository2 = "TestRepository";
                public const string LoginFailMessage1 = "Username or password is invalid";

              
    }
}
