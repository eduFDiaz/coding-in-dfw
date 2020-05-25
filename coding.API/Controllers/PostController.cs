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
using System.Linq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using coding.API.Models.Posts.Comments;
using coding.API.Dtos.Posts;
using coding.API.Models.Photos;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly Repository<Post> _postDal;

        private readonly Repository<PostPhoto> _postPhotoDal;
        private readonly Repository<Tag> _tagDal;
        private readonly Repository<PostTag> _postTagDal;

        public PostController(
        Repository<PostTag> postTagDal,
        Repository<PostPhoto> postPhotoDal,
        Repository<Tag> tagDal,
        Repository<Post> postDal,
        IConfiguration config, IMapper mapper)
        {

            _postTagDal = postTagDal;
            _postPhotoDal = postPhotoDal;
            _postDal = postDal;
            _tagDal = tagDal;

            _config = config;
            _mapper = mapper;
        }

        // [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PostForCreateDto request)
        {

            var postForCreate = _mapper.Map<Post>(request);

            // crea el post ahora
            var createdPost = await _postDal.Add(postForCreate);

            // Itero por los ids que recibo del usuario
            // if (request.TagId.Count > 0) {
            foreach (var tag in request.PostTagId)
            {
                // crea la tabla m2m
                var postag = new PostTag
                {
                    TagId = tag,
                    PostId = createdPost.Id // dame la id del post creado
                };

                // guarda la partida
                await _postTagDal.Add(postag);
            }
            // }



            return Ok(new PostPresenter(createdPost));

        }


        // [Authorize]
        [HttpGet("foruser/{userId}", Name = "GetPost")]
        public async Task<IActionResult> GetAllPostsForUser(Guid userId)
        {

            var allUserPosts = (await _postDal.GetRelatedFields("PostTags.Tag", "Comments")).ToList();

            var photos = (await _postDal.GetRelatedField("Photos")).Where(p => p.UserId == userId).ToList();

            var outPut = _mapper.Map<List<PostAllCommentDetailDto>>(allUserPosts);

            return Ok(outPut);
        }


        [HttpDelete("{postid}/delete", Name = "DetelePost")]
        public async Task<IActionResult> DeletePost(Guid postid)
        {
            var postToDelete = (await _postDal.GetById(postid));

            if (postToDelete == null)
                return NotFound();

            if (await _postDal.Delete(postToDelete))
                return NoContent();

            return BadRequest("Cant Delete the post!");

        }


        [HttpPut("{postid}/update", Name = "Update Post")]
        public async Task<IActionResult> UpdatePost(Guid postid, [FromBody] PostForUpdateDto postForUpdateDto)
        {
            var postToUpdateFromRepo = (await _postDal.GetById(postid));

            var postTagsFromRepo = (await _postTagDal.ListAsync()).Where(pt => pt.PostId == postid).ToList();

            if (postToUpdateFromRepo == null)
                return NotFound();

            var toUpd = _mapper.Map(postForUpdateDto, postToUpdateFromRepo);

            if (await _postDal.Update(toUpd))
                return NoContent();

            return BadRequest("cant update the post!");
        }

        [HttpGet("{postid}", Name = "Get Single Post")]
        public async Task<IActionResult> GetPost(Guid postid)
        {
            var singlePostFromRepo = (await _postDal.GetByIdWithList(postid, "PostTags.Tag", "Comments"));

            var postPhotos = (await _postPhotoDal.ListAsync()).Where(p => p.PostId == postid).ToList();

            if (singlePostFromRepo == null)
                return NotFound();

            var outPut = _mapper.Map<PostAllCommentDetailDto>(singlePostFromRepo);

            return Ok(outPut);

        }
    }
}