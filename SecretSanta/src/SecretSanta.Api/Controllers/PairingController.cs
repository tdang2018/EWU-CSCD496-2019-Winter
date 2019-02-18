using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Api.Controllers
{
    public class PairingController : ControllerBase
    {
        private IPairingService PairingService { get; set; }
        private IMapper Mapper { get; set; }

        public PairingController(IPairingService pairingService, IMapper mapper)
        {
            PairingService = pairingService;
            Mapper = mapper;
        }

        // POST api/pairing/5
        [HttpPost("{groupId}")]
        [Produces(typeof(ICollection<PairingViewModel>))]
        public async Task<IActionResult> GenerateUserPairings(int groupId)
        {
            if (groupId <= 0)
            {
                return BadRequest();
            }

            var pairings = await PairingService.GenerateUserPairings(groupId);
            return Created($"/pairing/{groupId}", pairings.Select(p => Mapper.Map<PairingViewModel>(p)).ToList());
        }

    }
}