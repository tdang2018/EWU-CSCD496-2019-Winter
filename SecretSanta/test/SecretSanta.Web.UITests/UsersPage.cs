using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SecretSanta.Web.UITests
{
    public class UsersPage
    {
        public const string Slug = "Users";

        public IWebDriver Driver { get; }

        public IWebElement AddUser => Driver.FindElement(By.LinkText("Add User"));

        public IWebElement EditButton => Driver.FindElement(By.LinkText("Edit"));

        public AddUsersPage AddUsersPage => new AddUsersPage(Driver);

        public List<string> UserNames
        {
            get
            {
                var elements = Driver.FindElements(By.CssSelector("h1+ul>li"));

                return elements
                    .Select(x =>
                    {
                        var text = x.Text;
                        if (text.EndsWith(" Edit Delete"))
                        {
                            text = text.Substring(0, text.Length - " Edit Delete".Length);
                        }
                        return text;
                    })
                    .ToList();
            }
        }

        public IWebElement GetDeleteLink(string userName)
        {
            ReadOnlyCollection<IWebElement> deleteLinks =
                Driver.FindElements(By.CssSelector("a.is-danger"));

            return deleteLinks.Single(x => x.GetAttribute("onclick").EndsWith($"{userName}?')"));
        }

        public UsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}
