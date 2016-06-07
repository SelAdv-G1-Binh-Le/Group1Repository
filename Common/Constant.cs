using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace Group1Project.Common
{
    public class Constant
    {
        //Diep Logigear: http://192.168.170.37/TADashboard/login.jsp
        //Diep Home: http://192.168.1.2/TADashboard/login.jsp
        //Binh Home: http://192.168.1.2:54000/TADashboard/login.jsp
        //Global home page: http://groupba.dyndns.org:54000/TADashboard/login.jsp

        #region Configuration variable area

        public const bool DebugMode = false;
        public const string LoginPageURL = "http://192.168.1.2/TADashboard/login.jsp";
        public const string DefaultUsername = "administrator";
        public const string DefaultPassword = "";
        public const string DefaultRepository = "SampleRepository";
        public const int DefaultTimeout = 5; 

        #endregion

        #region Data for Testing

        public const string InvalidUsername = "abc";
        public const string InvalidPassword = "abc";
        public const string UserName2 = "test";
        public const string PassWord2 = "admin";
        public const string Repository2 = "TestRepository";
        public const string LoginFailMessage1 = "Username or password is invalid";
        public const string LoginFailMessage2 = "Please enter username";
        public const string DefaultDisplayName = "DiepDuong";
        public const string DefaultSeriesValue = "name";
        public const string DefaultChartType = "Pie";
        public const string panelTC33 = "giang - panel";
        public const string dynamicxPathTC38 = "//li[@class='edit']"; 

        #endregion


    }



}
