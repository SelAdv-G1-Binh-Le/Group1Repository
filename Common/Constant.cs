using OpenQA.Selenium;
using System;

namespace Group1Project.Common
{
    class Constant
    {
        public static IWebDriver WebDriver;
        public const string LoginPageURL = "http://192.168.1.2/TADashboard/login.jsp";
        public const string DefaultUsername = "administrator";
        public const string DefaultPassword = "";
        public const string InvalidUsername = "abc";
        public const string InvalidPassword = "abc";
        public const string DefaultRepository = "SampleRepository";
        public const string Repository2 = "TestRepository";

              
    }
}
