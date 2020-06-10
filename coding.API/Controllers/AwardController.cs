using AutoMapper;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

using coding.API.Models.Awards;


using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AwardController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly Repository<Award> _awardDal;

        public AwardController(
            Repository<Award> awardDal, IConfiguration config, IMapper mapper)
        {

            _awardDal = awardDal;
            _config = config;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAwardDto request)
        {
            var awardToCreate = _mapper.Map<Award>(request);

            var createdaward = await _awardDal.Add(awardToCreate);

            return Ok(new AwardPresenter(createdaward));

        }


        
        [HttpGet("foruser/{userId}", Name = "Get award for User")]
        public async Task<IActionResult> GetawardForUser(Guid userId)
        {

            var allUserawards = (await _awardDal.ListAsync()).Where(p => p.UserId == userId).ToList();

            return Ok(allUserawards);

        }

        [Authorize]
        [HttpDelete("{awardid}/delete", Name = "Deleteaward")]
        public async Task<IActionResult> DeleteLan(Guid awardid)
        {
            var awardToDelete = (await _awardDal.GetById(awardid));

            if (awardToDelete == null)
                return NotFound();

            if (await _awardDal.Delete(awardToDelete))
                return NoContent();

            return BadRequest("Can't erase the award");


        }

        [Authorize]
        [HttpPut("{awardid}/update", Name = "Update award")]
        public async Task<IActionResult> UpdateAward(Guid awardid, [FromBody] UpdateAwardDto request)
        {
            var awardToUpdate = (await _awardDal.GetById(awardid));

            if (awardToUpdate == null)
                return NotFound();

            var toUpd = _mapper.Map(request, awardToUpdate);

            if (await _awardDal.Update(toUpd))
                return NoContent();

            return BadRequest("cant update the award!");

        }

        

    }
}