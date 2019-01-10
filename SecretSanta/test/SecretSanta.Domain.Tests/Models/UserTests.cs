using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.Models
{
   [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void MyTestMethod()
        {
            User u = new User { FirstName = "Tuan", LastName = "Dang" };
            Assert.AreEqual("Tuan", u.FirstName);
        }

    }
}
