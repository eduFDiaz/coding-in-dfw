using AutoMapper;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

using coding.API.Models.Educations;


using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly Repository<Education> _educationDal;
       
        public EducationController(
            Repository<Education> educationDal, IConfiguration config, IMapper mapper)
        {
            
             _educationDal = educationDal;
             _config = config;
            _mapper = mapper;
        }

        // [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateEducationDto request)
        {
            var educationToCreate = _mapper.Map<Education>(request);

            var createdEducation = await _educationDal.Add(educationToCreate);

            return Ok(new EducationPresenter(createdEducation));

        } 


        
        [HttpGet("foruser/{userId}", Name = "Get Education for User")]
        public async Task<IActionResult> GetEducationForUser(Guid userId)
        {

            var allUserEducations = (await _educationDal.ListAsync()).Where(p => p.UserId == userId).ToList();

             return Ok(allUserEducations);
        
        }

        
        [HttpDelete("{Educationid}/delete", Name = "DeleteEducation")]
        public async Task<IActionResult> DeleteLan(Guid Educationid)
        {
           var EducationToDelete = (await _educationDal.GetById(Educationid));

             if (EducationToDelete == null)
                  return NotFound();

            await _educationDal.Delete(EducationToDelete);
                
            if (await _educationDal.SaveAll())    
                 return NoContent();

            return BadRequest("Catn erase the Education");
            

        }

        
        [HttpPut("{Educationid}/update", Name = "Update Education")]
        public async Task<IActionResult> UpdateLan(Guid Educationid, [FromBody] UpdateEducationDto request)
        {
            var educationToUpdate = (await _educationDal.GetById(Educationid));

            if (educationToUpdate == null)
                return NotFound();

            var toUpd = _mapper.Map(request, educationToUpdate);
            
            // educationToUpdate.Title = request.Title;
            // educationToUpdate.DateRange = request.DateRange;
            // educationToUpdate.SchoolName = request.SchoolName;

            _educationDal.Update(toUpd);

            if (await _educationDal.SaveAll())
                return NoContent();

            return BadRequest("cant update the Education!");
            
        }

    }
}