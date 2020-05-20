using AutoMapper;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

using coding.API.Models.Projects;


using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly Repository<Project> _projectDal;

        public ProjectController(
            Repository<Project> projectDal, IConfiguration config, IMapper mapper)
        {

            _projectDal = projectDal;
            _config = config;
            _mapper = mapper;
        }

        // [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto request)
        {
            var projectToCreate = _mapper.Map<Project>(request);

            var createdProject = await _projectDal.Add(projectToCreate);

            return Ok(new ProjectPresenter(projectToCreate));

        }



        [HttpGet("foruser/{userId}", Name = "Get project for User")]
        public async Task<IActionResult> GetProjectForUser(Guid userId)
        {

            var allUserprojects = (await _projectDal.ListAsync()).Where(p => p.UserId == userId).ToList();

            return Ok(allUserprojects);

        }


        [HttpDelete("{projectid}/delete", Name = "DeleteProject")]
        public async Task<IActionResult> DeleteLan(Guid projectid)
        {
            var projectToDelete = (await _projectDal.GetById(projectid));

            if (projectToDelete == null)
                return NotFound();

            if (await _projectDal.Delete(projectToDelete))
                return NoContent();

            return BadRequest("Catn erase the project");
        }


        [HttpPut("{projectid}/update", Name = "Update Project")]
        public async Task<IActionResult> UpdateLan(Guid projectid, [FromBody] UpdateProjectDto request)
        {
            var prjectToUpdate = (await _projectDal.GetById(projectid));

            if (prjectToUpdate == null)
                return NotFound();

            // prjectToUpdate.Title = request.Title;
            // prjectToUpdate.Resume = request.Resume;
            // prjectToUpdate.Type = request.Type;

            var toUpd = _mapper.Map(request, prjectToUpdate);

            if (await _projectDal.Update(toUpd))
                return NoContent();

            return BadRequest("cant update the project!");

        }

    }
}