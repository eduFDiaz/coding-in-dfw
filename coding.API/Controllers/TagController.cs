using coding.API.Models.Entities.Tags;
using System.Collections.Generic;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
 

using coding.API.Models.Interfaces;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController: ControllerBase
    {
        private readonly ITagRepo _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public TagController(ITagRepo repo, IConfiguration config, IMapper mapper)
        {
            this._repo = repo;
            this._config = config;
            this._mapper = mapper;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<Tag> Create([FromBody] TagForCreateDto tagForCreateDto)
        {

            var tagToCreate = _mapper.Map<Tag>(tagForCreateDto);

            var createdTag = await _repo.Create(tagToCreate);

            return createdTag;
            
        }

     
        [Authorize]
        [HttpDelete("{tagid}/delete", Name = "DeleteTag")]
        public async Task<IActionResult> DeleteTag(int tagid)
        {
                           
            var tagFromRepo = await _repo.DeleteTag(tagid);

            if (!tagFromRepo)
                return NotFound();
    
            return NoContent();
                        
        }

        [Authorize]
        [HttpPut("{tagid}/update", Name = "Update Tag")]
        public async Task<IActionResult> UpdateTag(int tagid, [FromBody] TagForUpdateDto tagForUpdateDto)
        {
            var tagFromRepo = await _repo.GetTag(tagid);

            if (tagFromRepo == null)
                return NotFound();
                
            _mapper.Map(tagForUpdateDto, tagFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Failed update Tag");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tagsFromRepo = await _repo.GetAll();

            return Ok(tagsFromRepo);
        }
    }
}