using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly IGiftService _GiftService;

        public GiftController(IGiftService giftService)
        {
            _GiftService = giftService ?? throw new ArgumentNullException(nameof(giftService));
        }

        // GET api/Gift/5
        [HttpGet("{userId}")]
        public ActionResult<List<DTO.Gift>> GetGiftForUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            List<Gift> databaseUsers = _GiftService.GetGiftsForUser(userId);
            
            return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
        }

        //POST api/Gift/4
        [HttpPost("{userId}")]
        public ActionResult AddGiftToUser(DTO.Gift gift, int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            if (gift == null)
            {
                return BadRequest();
            }

            _GiftService.AddGiftToUser(userId, DTO.Gift.ToEntity(gift));
            return Ok();            
        }


        // PUT api/values/5
        [HttpPut("{userId}")]
        public ActionResult UpdateGiftToUser(DTO.Gift gift, int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            if (gift == null)
            {
                return BadRequest();
            }

            _GiftService.UpdateGiftForUser(userId, DTO.Gift.ToEntity(gift));
                
            return Ok();
        }

        [HttpDelete]
        public ActionResult RemoveGift(DTO.Gift gift)
        {
            if (gift == null)
            {
                return BadRequest();
            }

            _GiftService.RemoveGift(DTO.Gift.ToEntity(gift));

            return Ok();
        }


    }
}
