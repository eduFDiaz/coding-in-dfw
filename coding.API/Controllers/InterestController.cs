using AutoMapper;
using coding.API.Data;
using coding.API.Dtos;

using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

using coding.API.Models.Interests;


using System;

using System.Threading.Tasks;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterestController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly IRepository<Interest> _interestDal;

        public InterestController(
            IRepository<Interest> interestDal, IConfiguration config, IMapper mapper)
        {

            _interestDal = interestDal;
            _config = config;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateInterestDto request)
        {
            var interestToCreate = _mapper.Map<Interest>(request);

            var createdinterest = await _interestDal.Add(interestToCreate);

            return Ok(new InterestPresenter(createdinterest));

        }



        [HttpGet("foruser/{userId}", Name = "Get interest for User")]
        public async Task<IActionResult> GetinterestForUser(Guid userId)
        {

            var allUserinterests = (await _interestDal.ListAsync()).Where(p => p.UserId == userId).ToList();

            return Ok(allUserinterests);

        }

        [Authorize]
        [HttpDelete("{interestid}/delete", Name = "Deleteinterest")]
        public async Task<IActionResult> DeleteLan(Guid interestid)
        {
            var interestToDelete = (await _interestDal.GetById(interestid));

            if (interestToDelete == null)
                return NotFound();

            if (await _interestDal.Delete(interestToDelete))
                return NoContent();

            return BadRequest("Catn erase the interest");


        }

        [Authorize]
        [HttpPut("{interestid}/update", Name = "Update interest")]
        public async Task<IActionResult> UpdateInterest(Guid interestid, [FromBody] UpdateInterestDto request)
        {
            var interestToUpdate = (await _interestDal.GetById(interestid));

            if (interestToUpdate == null)
                return NotFound();

            var toUpd = _mapper.Map(request, interestToUpdate);

            

            if (await _interestDal.Update(toUpd))
                return NoContent();

            return BadRequest("cant update the interest!");

        }

    }
}