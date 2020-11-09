using AutoMapper;
using coding.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using coding.API.Models.Subscribers;
using System;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsletterController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IRepository<Subscriber> _subDal;

        public NewsletterController(
            IRepository<Subscriber> subDal, IConfiguration config)
        {

            _subDal = subDal;
            _config = config;
            

        }

       // [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Subscriber request)
        {
            Subscriber subToCreate = new Subscriber();

            subToCreate.Email = request.Email;
            Subscriber subCreated =  await _subDal.Add(subToCreate);
        

            if (subCreated is Subscriber)
                return Ok(subCreated);

            return BadRequest("Can't create the subscription");

        }

        //[Authorize]
        [HttpDelete("{subId}/delete", Name = "Delete Subcription")]
        public async Task<IActionResult> DeleteMessage(Guid subId)
        {
            var sb = (await _subDal.GetById(subId));

            if (sb == null)
                return NotFound();

            if (await _subDal.Delete(sb))
                return NoContent();

            return BadRequest("Cant delete the subcription!");

        }

        

        [HttpGet("all", Name = "Return all subcriptions")]
        public async Task<IActionResult> GetAllSubcriptions()
        {
            ICollection<Subscriber> subscribers = (await _subDal.ListAsync())
            .ToList();

            return Ok(subscribers);
        }
        


    }
}
