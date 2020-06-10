using AutoMapper;
using coding.API.Data;
using coding.API.Dtos.Comments;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

using System.Collections.Generic;
using System.Threading.Tasks;
using coding.API.Models.Messages;
using coding.API.Dtos;
using coding.API.Models.FeatureSkills;
using System;
using Microsoft.AspNetCore.Authorization;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeatureSkillController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly Repository<FeatureSkill> _featureSkillDal;

        public FeatureSkillController(
            Repository<FeatureSkill> featureSkillDal, IConfiguration config, IMapper mapper)
        {

            _featureSkillDal = featureSkillDal;
            _config = config;
            _mapper = mapper;

        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateFeatureSkillDto request)
        {
            FeatureSkill featureSkill = _mapper.Map<FeatureSkill>(request);

            FeatureSkill createdfeatureSkill = await _featureSkillDal.Add(featureSkill);

            return Ok(new FeatureSkillPresenter(createdfeatureSkill));

        }
        [Authorize]
        [HttpPut("{ftid}/update", Name = "Update FeatureSkill")]
        public async Task<IActionResult> UpdateFeatureSkill(Guid ftid, [FromBody] UpdateFeatureSkillDto request)
        {
            FeatureSkill ftToUpdate = (await _featureSkillDal.GetById(ftid));

            if (ftToUpdate == null)
                return NotFound();
            FeatureSkill updatedFSkill = _mapper.Map(request, ftToUpdate);

            if (await _featureSkillDal.Update(updatedFSkill))
                return Ok(new FeatureSkillPresenter(updatedFSkill));

            return BadRequest("cant update the feature skill!");

        }

        [HttpGet("all", Name = "Return all feature skills")]
        public async Task<IActionResult> GetAllFeatureSkills()
        {
            IEnumerable<FeatureSkill> featureSkills = (await _featureSkillDal.ListAsync())
            .ToList().OrderByDescending(fs => fs.DateCreated).Take(10);

            return Ok(featureSkills);
        }
        [Authorize]
        [HttpDelete("{ftid}/delete", Name = "Delete FeatureSkill")]
        public async Task<IActionResult> DeleteFeatureSkill(Guid ftid)
        {
            var fs = (await _featureSkillDal.GetById(ftid));

            if (fs == null)
                return NotFound();

            if (await _featureSkillDal.Delete(fs))
                return NoContent();

            return BadRequest("Cant delete the feature!");

        }

    }
}

