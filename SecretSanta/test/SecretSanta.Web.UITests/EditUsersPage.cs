using OpenQA.Selenium;
using System;
using System.Linq;

namespace SecretSanta.Web.UITests
{
    public class EditUsersPage
    {
        IWebDriver Driver { get; }
        public IWebElement FirstNameTextBox => Driver.FindElement(By.Id("FirstName"));
        public IWebElement LastNameTextBox => Driver.FindElement(By.Id("LastName"));
        public string CurrentUserID =>
            Driver.Url.Substring(Driver.Url.LastIndexOf("/") + 1);
        public IWebElement SubmitButton =>
            Driver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");

        public EditUsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}
