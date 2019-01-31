using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.DTO;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.Controllers
{
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _GroupService;

        public GroupController(IGroupService groupService)
        {
            _GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        //Post api/Group
        [HttpPost]
        public ActionResult CreateGroup(DTO.Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            _GroupService.AddGroup(DTO.Group.ToDomain(group));
            return Ok();
        }

        // PUT api/Group
        [HttpPut]
        public ActionResult UpdateGroup(DTO.Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            _GroupService.UpdateGroup(DTO.Group.ToDomain(group));

            return Ok();
        }

        [HttpDelete]
        public ActionResult RemoveGroup(DTO.Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            _GroupService.DeleteGroup(DTO.Group.ToDomain(group));

            return Ok();
        }

        [HttpGet]
        public ActionResult<List<DTO.Group>> GetAllGroups()
        {
            List<Domain.Models.Group> databaseGroups = _GroupService.FetchAll();
            if (databaseGroups == null)
                return BadRequest();
            return databaseGroups.Select(x => new DTO.Group(x)).ToList();
        }

        [HttpGet("{groupId}")]
        public ActionResult<List<DTO.User>> FetchAllUsersInGroup(int groupId)
        {
            if (groupId <= 0) return NotFound();

            return _GroupService.FetchAllUsersInGroup(groupId).Select(user => new DTO.User(user)).ToList();
        }

        [HttpPost("{groupId}")]
        public ActionResult AddUserToGroup(int groupId, DTO.User user)
        {
            if (groupId <= 0) return NotFound();
            if (user == null) return BadRequest();

            _GroupService.AddUserToGroup(groupId, DTO.User.ToDomain(user));
            return Ok();
        }

        [HttpDelete("{groupId}")]
        public ActionResult RemoveUserFromGroup(int groupId, DTO.User user)
        {
            if (groupId <= 0) return NotFound();
            if (user == null) return BadRequest();

            _GroupService.RemoveUserFromGroup(groupId, DTO.User.ToDomain(user));
            return Ok();
        }
    }
}