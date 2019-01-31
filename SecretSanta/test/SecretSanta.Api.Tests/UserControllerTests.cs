using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserController_RequiresUserService()
        {
            new UserController(null);
        }

        [TestMethod]
        public void Createuser_RequiresUser()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);

            ActionResult result = controller.CreateUser(null);

            Assert.IsTrue(result is BadRequestResult);           
            
        }

        [TestMethod]
        public void CreateUser_InvokesService()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);
            var userDto = new DTO.User {Id =1 ,  FirstName="Tuan", LastName="Dang" };

            ActionResult result = controller.CreateUser(userDto);

            OkResult okResult = result as OkResult;
            Assert.IsNotNull(result, "Result was not a 200");
            Assert.AreEqual(userDto.Id, testService.AddUser_User.Id);
            Assert.AreEqual(userDto.FirstName, testService.AddUser_User.FirstName);
            Assert.AreEqual(userDto.LastName, testService.AddUser_User.LastName);
        }

        [TestMethod]
        public void UpdateUser_RequiresUser()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);

            ActionResult result = controller.UpdateUser(null);

            Assert.IsTrue(result is BadRequestResult);

        }

        [TestMethod]
        public void UpdateUser_InvokesService()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);
            var userDto = new DTO.User { Id = 1, FirstName = "Richard", LastName = "Teller" };

            ActionResult result = controller.UpdateUser(userDto);

            OkResult okResult = result as OkResult;
            Assert.IsNotNull(result, "Result was not a 200");
            Assert.AreEqual(userDto.Id, testService.UpdateUser_User.Id);
            Assert.AreEqual(userDto.FirstName, testService.UpdateUser_User.FirstName);
            Assert.AreEqual(userDto.LastName, testService.UpdateUser_User.LastName);
        }

        [TestMethod]
        public void DeleteUser_RequiresUser()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);

            ActionResult result = controller.RemoveUser(null);

            Assert.IsTrue(result is BadRequestResult);

        }

        [TestMethod]
        public void DeleteUser_InvokesService()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);
            var userDto = new DTO.User { Id = 1, FirstName = "Richard", LastName = "Teller" };

            ActionResult result = controller.RemoveUser(userDto);

            OkResult okResult = result as OkResult;
            Assert.IsNotNull(result, "Result was not a 200");
            Assert.AreEqual(userDto.Id, testService.DeleteUser_User.Id);
            Assert.AreEqual(userDto.FirstName, testService.DeleteUser_User.FirstName);
            Assert.AreEqual(userDto.LastName, testService.DeleteUser_User.LastName);
        }

    }
}
