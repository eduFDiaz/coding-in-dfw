using AutoMapper;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using coding.API.Models.Posts;
using coding.API.Models.Tags;
using coding.API.Models.PostTags;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly Repository<Tag> _tagDal;
        private readonly Repository<PostTag> _postTagDal;

        public TagController(
            Repository<PostTag> postTagDal,
            Repository<Tag> tagDal,
            IConfiguration config, IMapper mapper)
        {

            _postTagDal = postTagDal;

            _tagDal = tagDal;

            _config = config;
            _mapper = mapper;
        }

        /* [Authorize] */
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TagForCreateDto request)
        {
            var tag = new Tag
            {
                Title = request.Title,
                Description = request.Description,
            };

            // var postForCreate = _mapper.Map<Post>(request);

            // crea el post ahora
            var createdTag = await _tagDal.Add(tag);

            return Ok(new TagPresenter(createdTag));

        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllTags()
        {
            var alltags = (await _tagDal.ListAsync());

            var tagCount = alltags.Count;

            if (tagCount == 0)
                return NoContent();

            return Ok(alltags);
        }

        [Authorize]
        [HttpDelete("{tagid}/delete")]
        public async Task<IActionResult> DeleteTag(Guid tagid)
        {
            var tagToDelete = (await _tagDal.GetById(tagid));

            if (await _tagDal.Delete(tagToDelete))
                return NoContent();

            return BadRequest("cant delete the tag");

        }

        [Authorize]
        [HttpPut("{tagid}/update")]
        public async Task<IActionResult> UpdateTag(Guid tagid, [FromBody] TagForUpdateDto request)
        {
            var tag = (await _tagDal.GetById(tagid));

            var toUpd = _mapper.Map(request, tag);

            await _tagDal.Update(toUpd);

            if (await _tagDal.SaveAll())
                return NoContent();

            return BadRequest("Cant update the tag");

        }


    }
}