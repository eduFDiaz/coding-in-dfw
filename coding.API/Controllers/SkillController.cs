using AutoMapper;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

using coding.API.Models.Skills;


using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly Repository<Skill> _skillDal;
       
        public SkillController(
            Repository<Skill> skillDal, IConfiguration config, IMapper mapper)
        {
            
             _skillDal = skillDal;
             _config = config;
            _mapper = mapper;
        }

        // [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateSkillDto request)
        {
            var skillToCreate = _mapper.Map<Skill>(request);

            var createdskill = await _skillDal.Add(skillToCreate);

            return Ok(new SkillPresenter(createdskill));

        } 


        
        [HttpGet("foruser/{userId}", Name = "Get skill for User")]
        public async Task<IActionResult> GetskillForUser(Guid userId)
        {

            var allUserSkills = (await _skillDal.ListAsync()).Where(p => p.UserId == userId).ToList();

             return Ok(allUserSkills);
        
        }

        
        [HttpDelete("{Skillid}/delete", Name = "DeleteSkill")]
        public async Task<IActionResult> DeleteLan(Guid Skillid)
        {
           var SkillToDelete = (await _skillDal.GetById(Skillid));

             if (SkillToDelete == null)
                  return NotFound();

            await _skillDal.Delete(SkillToDelete);
                
            if (await _skillDal.SaveAll())    
                 return NoContent();

            return BadRequest("Catn erase the Skill");
            

        }

        
        [HttpPut("{Skillid}/update", Name = "Update Skill")]
        public async Task<IActionResult> UpdateSkill(Guid Skillid, [FromBody] UpdateSkillDto request)
        {
            var SkillToUpdate = (await _skillDal.GetById(Skillid));

            if (SkillToUpdate == null)
                return NotFound();
            
            var updatedSkill = _mapper.Map(request, SkillToUpdate);

            _skillDal.Update(updatedSkill);

            if (await _skillDal.SaveAll())
                return NoContent();

            return BadRequest("cant update the Skill!");
            
        }

    }
}