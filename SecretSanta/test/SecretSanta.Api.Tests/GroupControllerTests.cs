using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class GroupControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupController_RequiresGroupService()
        {
            new GroupController(null);
        }

        [TestMethod]
        public void CreateGroup_RequiresGroup()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult result = controller.CreateGroup(null);

            Assert.IsTrue(result is BadRequestResult);

        }

        [TestMethod]
        public void CreateGroup_InvokesService()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var groupDto = new DTO.Group { Id = 1, Name ="Group 1"};

            ActionResult result = controller.CreateGroup(groupDto);

            OkResult okResult = result as OkResult;
            Assert.IsNotNull(result, "Result was not a 200");
            Assert.AreEqual(groupDto.Id, testService.AddGroup_Group.Id);            
            Assert.AreEqual(groupDto.Name, testService.AddGroup_Group.Name);     
        }

        [TestMethod]
        public void UpdateGroup_RequiresGroup()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult result = controller.UpdateGroup(null);

            Assert.IsTrue(result is BadRequestResult);

        }

        [TestMethod]
        public void UpdateGroup_InvokesService()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var groupDto = new DTO.Group { Id = 1, Name = "Group 1" };

            ActionResult result = controller.UpdateGroup(groupDto);

            OkResult okResult = result as OkResult;
            Assert.IsNotNull(result, "Result was not a 200");
            Assert.AreEqual(groupDto.Id, testService.UpdateGroup_Group.Id);
            Assert.AreEqual(groupDto.Name, testService.UpdateGroup_Group.Name);
        }

        [TestMethod]
        public void DeleteGroup_RequiresGroup()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult result = controller.RemoveGroup(null);

            Assert.IsTrue(result is BadRequestResult);

        }

        [TestMethod]
        public void DeleteGroup_InvokesService()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var groupDto = new DTO.Group { Id = 1, Name = "Group 1" };

            ActionResult result = controller.RemoveGroup(groupDto);

            OkResult okResult = result as OkResult;
            Assert.IsNotNull(result, "Result was not a 200");
            Assert.AreEqual(groupDto.Id, testService.DeleteGroup_Group.Id);
            Assert.AreEqual(groupDto.Name, testService.DeleteGroup_Group.Name);
        }

        [TestMethod]
        public void FetchAllGroups_ReturnsAllGroups()
        {
            var testService = new TestableGroupService()
            {
                FetchAll_Return = new List<Group>
                {
                    new Group { Id = 1, Name = "Group 1" },
                    new Group { Id = 2, Name = "Group 2" }
                }
              };
            var controller =  new GroupController(testService);
            var listGroupDto = new List<DTO.Group>
            {
                new DTO.Group { Id = 1, Name = "Group 1" },
                new DTO.Group { Id = 2, Name = "Group 2" }
            };           
           
            var result = controller.GetAllGroups();
            var groups = result.Value;

            Assert.AreEqual(listGroupDto.Count, groups.Count);
            Assert.AreEqual(listGroupDto[0].Id, groups[0].Id);
            Assert.AreEqual(listGroupDto[1].Id, groups[1].Id);
        }

        [TestMethod]
        public void FetchAllGroups_ReturnsNull()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);          

            var result = controller.GetAllGroups();
            var groups = result.Value;           

            Assert.AreEqual(null, groups);
        }

        [TestMethod]
        public void FetchAllUsersFromGroup_GroupIdPositive()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var result = controller.FetchAllUsersInGroup(-1);
            Assert.IsTrue(result.Result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.FetchAllUsersInGroup_groupId);            
        }

        [TestMethod]
        public void FetchAllUsersFromGroup_UsersAreFetched()
        {
            var testService = new TestableGroupService
            {
                FetchAllUsersInGroup_Return= new List<User> { new User { Id = 5, FirstName = "Grant", LastName = "Woods" } }
            };
            var controller = new GroupController(testService);
            var usersReturned = controller.FetchAllUsersInGroup(5).Value;
            Assert.AreEqual(1, usersReturned.Count);
            Assert.AreEqual(5, usersReturned[0].Id);
            Assert.AreEqual("Grant", usersReturned[0].FirstName);
            Assert.AreEqual("Woods", usersReturned[0].LastName);      

        }

        [TestMethod]
        public void AddUserToGroup_RequiresUser()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var result = controller.AddUserToGroup(4, null);
            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void AddUserToGroup_GroupIdPositive()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var result = controller.AddUserToGroup(-1, new DTO.User());
            Assert.IsTrue(result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.AddUserToGroup_groupId);            
        }

        [TestMethod]
        public void AddUserToGroup_InvokesService()
        {
            var userDto = new DTO.User { Id = 4, FirstName = "Kenny", LastName = "White" };
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult result = controller.AddUserToGroup(5, userDto);

            OkResult okResult = result as OkResult;

            Assert.IsNotNull(result, "Result was not a 200");
            Assert.AreEqual(5, testService.AddUserToGroup_groupId);
            Assert.AreEqual(userDto.Id, testService.AddUserToGroup_User.Id);           
        }


        [TestMethod]
        public void RemoveUserFromGroup_RequiresUser()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var result = controller.RemoveUserFromGroup(4, null);
            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void RemoveUserFromGroup_GroupIdPositive()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var result = controller.RemoveUserFromGroup(-1, new DTO.User());
            Assert.IsTrue(result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.RemoveUserFromGroup_groupId);
        }

        [TestMethod]
        public void RemoveUserFromGroup_InvokesService()
        {
            var userDto = new DTO.User { Id = 4, FirstName = "Kenny", LastName = "White" };
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult result = controller.RemoveUserFromGroup(5, userDto);

            OkResult okResult = result as OkResult;

            Assert.IsNotNull(result, "Result was not a 200");
            Assert.AreEqual(5, testService.RemoveUserFromGroup_groupId);
            Assert.AreEqual(userDto.Id, testService.RemoveUserFromGroup_User.Id);
        }
    }
}
