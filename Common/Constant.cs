﻿using OpenQA.Selenium;
using System;

namespace Group1Project.Common
{
    class Constant
    {
         //Diep Logigear: http://192.168.170.37:54001/TADashboard/login.jsp
         //Diep Home: http://192.168.1.2/TADashboard/login.jsp

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
