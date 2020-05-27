using AutoMapper;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

using coding.API.Models.WorkExperiences;


using System;
using System.Threading.Tasks;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkExperienceController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly Repository<WorkExperience> _workExperienceDal;

        public WorkExperienceController(
            Repository<WorkExperience> workExperienceDal, IConfiguration config, IMapper mapper)
        {

            _workExperienceDal = workExperienceDal;
            _config = config;
            _mapper = mapper;
        }

        // [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateWorkExperienceDto request)
        {
            var workExperienceToCreate = _mapper.Map<WorkExperience>(request);

            var createdworkExperience = await _workExperienceDal.Add(workExperienceToCreate);

            return Ok(new WorkExperiencePresenter(createdworkExperience));

        }



        [HttpGet("foruser/{userId}", Name = "Get workExperience for User")]
        public async Task<IActionResult> GetworkExperienceForUser(Guid userId)
        {

            var allUserworkExperiences = (await _workExperienceDal.ListAsync()).Where(p => p.UserId == userId).ToList();

            return Ok(allUserworkExperiences);

        }


        [HttpDelete("{workExperienceid}/delete", Name = "DeleteworkExperience")]
        public async Task<IActionResult> DeleteLan(Guid workExperienceid)
        {
            var workExperienceToDelete = (await _workExperienceDal.GetById(workExperienceid));

            if (workExperienceToDelete == null)
                return NotFound();

            if (await _workExperienceDal.Delete(workExperienceToDelete))
                return NoContent();

            return BadRequest("Catn erase the workExperience");

        }


        [HttpPut("{workExperienceid}/update", Name = "Update workExperience")]
        public async Task<IActionResult> UpdateworkExperience(Guid workExperienceid, [FromBody] UpdateWorkExperienceDto request)
        {
            var workExperienceToUpdate = (await _workExperienceDal.GetById(workExperienceid));

            if (workExperienceToUpdate == null)
                return NotFound();

            var toUpd = _mapper.Map(request, workExperienceToUpdate);

            if (await _workExperienceDal.Update(toUpd))
                return NoContent();

            return BadRequest("cant update the workExperience!");

        }

    }
}