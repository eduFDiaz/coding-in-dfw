using AutoMapper;
using coding.API.Data;
using coding.API.Dtos;

using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

using coding.API.Models.Languages;


using System;

using System.Threading.Tasks;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly IRepository<Language> _languageDal;

        public LanguageController(
            IRepository<Language> languageDal, IConfiguration config, IMapper mapper)
        {

            _languageDal = languageDal;
            _config = config;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateLanguageDto request)
        {

            var lan = new Language
            {
                Name = request.Name,
                UserId = request.UserId

            };

            var createdLan = await _languageDal.Add(lan);

            return Ok(new LanguagePresenter(lan));

        }



        [HttpGet("foruser/{userId}", Name = "Get Language for User")]
        public async Task<IActionResult> GetLanForUser(Guid userId)
        {

            var allUserLanguages = (await _languageDal.ListAsync()).Where(p => p.UserId == userId).ToList();

            return Ok(allUserLanguages);

        }

        [Authorize]
        [HttpDelete("{lanid}/delete", Name = "DeteleLan")]
        public async Task<IActionResult> DeleteLan(Guid lanid)
        {
            var lanToDelete = (await _languageDal.GetById(lanid));

            if (lanToDelete == null)
                return NotFound();

            if (await _languageDal.Delete(lanToDelete))
                return NoContent();

            return BadRequest("Catn erase the lang");

        }

        [Authorize]
        [HttpPut("{lanid}/update", Name = "Update Lan")]
        public async Task<IActionResult> UpdateLan(Guid lanid, [FromBody] UpdateLanguageDto request)
        {
            var lanToUpdate = (await _languageDal.GetById(lanid));

            if (lanToUpdate == null)
                return NotFound();

            var toUpd = _mapper.Map(request, lanToUpdate);

            if (await _languageDal.Update(toUpd))
                return NoContent();

            return BadRequest("cant update the language!");

        }


    }
}