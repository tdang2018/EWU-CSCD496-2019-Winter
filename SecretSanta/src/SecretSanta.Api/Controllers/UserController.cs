using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UserController(IUserService userService)
        {
            _UserService = userService ?? throw new ArgumentNullException(nameof(userService));
        }    

        //Post api/User
        [HttpPost]
        public ActionResult CreateUser(DTO.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _UserService.AddUser(DTO.User.ToDomain(user));
            return Ok();
        }

        // PUT api/user
        [HttpPut]
        public ActionResult UpdateUser(DTO.User user)
        {           
            if (user == null)
            {
                return BadRequest();
            }

            _UserService.UpdateUser(DTO.User.ToDomain(user));

            return Ok();
        }

        [HttpDelete]
        public ActionResult RemoveUser(DTO.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _UserService.DeleteUser(DTO.User.ToDomain(user));

            return Ok();
        }
    }
}