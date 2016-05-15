using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Group1Project.DataObjects
{
    public class MenuList
    {
        public MenuList()
        {}

        private string ParentMenu;

        public string ParentMenuObj
        {
            get { return ParentMenu; }
            set { ParentMenu = value; }
        }

        private string ChildMenu;

        public string ChildMenuObj
        {
            get { return ChildMenu; }
            set { ChildMenu = value; }
        }

        public enum MainMenuEnum
        {
            Test, GlobalSetting, Profile, Repository, Administer, UserName
        }

        public static string returnMainMenu(MainMenuEnum mainmn)
        {
            switch(mainmn)
            {
                case MainMenuEnum.Test: return "li[@class='active haschild']";
                case MainMenuEnum.GlobalSetting: return "li[@class='mn-setting']";
                case MainMenuEnum.Profile: return "a[@href='#Welcome']";
                case MainMenuEnum.Repository: return "a[@href='#Repository']";
                case MainMenuEnum.Administer: return "a[@href='#Administer']";
                case MainMenuEnum.UserName: return "a[@href='#Welcome']";
                default: return "";
            }
        }
        public enum ChildMenuEnum
        {
            TestChild, AddPage, CreateProfile, CreatePanel, Edit, Delete, MyProfile, Logout, TestRepository, DataProfiles, Panels
        }

        public static string returnChildMenu(ChildMenuEnum childmn)
        {
            switch(childmn)
            {
                case ChildMenuEnum.TestChild: return "Test Child";
                case ChildMenuEnum.AddPage: return "Add Page";
                case ChildMenuEnum.CreateProfile: return "Create Profile";
                case ChildMenuEnum.CreatePanel: return "Create Panel";
                case ChildMenuEnum.Edit: return "Edit";
                case ChildMenuEnum.Delete: return "Delete";
                case ChildMenuEnum.MyProfile: return "My Profile";
                case ChildMenuEnum.Logout: return "Logout";
                case ChildMenuEnum.TestRepository: return "TestRepository";
                case ChildMenuEnum.DataProfiles: return "Data Profiles";
                case ChildMenuEnum.Panels: return "Panels";
                default: return "";
            }
        }





    }
}
