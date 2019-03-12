using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SecretSanta.Web.UITests
{
    [TestClass]
    public class UserPageTests
    {
        private const string RootUrl = "https://localhost:44388/";

        private IWebDriver Driver { get; set; }

        [TestInitialize]
        public void Init()
        {
            var options = new ChromeOptions();
            options.BinaryLocation = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            Driver = new ChromeDriver(Path.GetFullPath("."), options);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        [TestMethod]
        public void CanGetToUsersPage()
        {
            //Arrange
            Driver.Navigate().GoToUrl(RootUrl);

            //Act
            var homePage = new HomePage(Driver);
            homePage.UsersLink.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
        }

        [TestMethod]
        public void CanNavigateToAddUsersPage()
        {
            //Arrange
            var rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, UsersPage.Slug));
            var page = new UsersPage(Driver);

            //Act
            page.AddUser.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(AddUsersPage.Slug));
        }


        [TestMethod]
        public void CanAddUsers()
        {
            //Arrange /Act
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");
            
           // string userName = "FirstName "
            UsersPage page = CreateUser(userFirstName, userLastName);

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            List<string> userNames = page.UserNames;
            //Assert.IsTrue(userNames.Contains(userFirstName + " " + userLastName));
            Assert.IsTrue(userNames.Contains($"{userFirstName} {userLastName}"));
        }

        [TestMethod]
        public void CanDeleteUser()
        {
            //Arrange
            string userFirstName = "First Name " + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name " + Guid.NewGuid().ToString("N");
            UsersPage page = CreateUser(userFirstName, userLastName);

            //Act
            IWebElement deleteLink = page.GetDeleteLink(userFirstName + " " + userLastName);
            deleteLink.Click();
            Driver.SwitchTo().Alert().Accept();
            //Assert
            List<string> userNames = page.UserNames;
            // Assert.IsFalse(userNames.Contains(userFirstName + " " + userLastName));
            Assert.IsFalse(userNames.Contains($"{userFirstName} {userLastName}"));
        }

        private UsersPage CreateUser(string userFirstName, string userLastName)
        {
            var rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, UsersPage.Slug));
            var page = new UsersPage(Driver);
            page.AddUser.Click();

            var addUserPage = new AddUsersPage(Driver);

            addUserPage.UserFirstNameTextBox.SendKeys(userFirstName);
            addUserPage.UserLastNameTextBox.SendKeys(userLastName);
            addUserPage.SubmitButton.Click();
            return page;
        }
    }
}
