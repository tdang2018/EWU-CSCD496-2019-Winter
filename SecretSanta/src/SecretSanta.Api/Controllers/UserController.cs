using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using SecretSanta.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService UserService { get; }

        private IMapper Mapper { get; }
  
        public UserController(IUserService userService, IMapper mapper)
        {
            UserService = userService ?? throw new ArgumentNullException(nameof(userService));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET api/User
        [HttpGet]
        [Produces(typeof(ICollection<UserViewModel>))]
        public IActionResult Get()
        {
            return Ok(UserService.FetchAll().Select(x => Mapper.Map<UserViewModel>(x)));
        }

        // POST api/User
        [HttpPost]
        [Produces(typeof(UserViewModel))]
        public IActionResult Post(UserInputViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return BadRequest();
            }
       
            var createdUser = UserService.AddUser(Mapper.Map<User>(userViewModel));
       
            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
            
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]        
        public IActionResult Put(int id, UserInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            var foundUser = UserService.Find(id);
            if (foundUser == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, foundUser);
            UserService.UpdateUser(foundUser);
            
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A User id must be specified");
            }

            bool userWasDeleted = UserService.DeleteUser(id);

            return userWasDeleted ? (ActionResult)Ok() : (ActionResult)NotFound();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Produces(typeof(UserViewModel))]
        public IActionResult Get(int id)
        {
            var foundUser = UserService.Find(id);
            if (foundUser == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<UserViewModel>(foundUser));
        }
    }
}
