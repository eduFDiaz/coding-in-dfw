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

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly Repository<Post> _postDal;
        private readonly Repository<Tag> _tagDal;
        private readonly Repository<PostTag> _postTagDal;

        private readonly Repository<Comment> _commentDal;

        public PostController(
            Repository<PostTag> postTagDal,
            Repository<Tag> tagDal,
            Repository<Post> postDal,
            IConfiguration config, IMapper mapper)
        {

            _postTagDal = postTagDal;
            _postDal = postDal;
            _tagDal = tagDal;

            _config = config;
            _mapper = mapper;
        }

        // [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PostForCreateDto request)
        {
            // var post = new Post
            // {
            //    Text = request.Text,
            //    Description = request.Description,
            //    Title = request.Title,
            //    UserId = request.UserId,
            //    ReadingTime = request.ReadingTime
            //    // @Denis el resto de los datos del post faltan aqu�
            // };

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

            var allUserPosts = (await _postDal.ListAsync()).Where(p => p.UserId == userId).ToList();

            var TagsList = new List<Tag>();

            // foreach (var post in allUserPosts)
            // {
            //     var test = (await _postTagDal.GetById(post.Id)).FirstOrDefault();

            //     if ( test != null ) {

            //     var tag = (await _tagDal.GetById(test.TagId));

            //     TagsList.Add(tag);

            //     }


            //   
            // }


            return Ok(allUserPosts);
        }

        [Authorize]
        [HttpDelete("{postid}/delete", Name = "DetelePost")]
        public async Task<IActionResult> DeletePost(Guid postid)
        {
            var postToDelete = (await _postDal.GetById(postid));

            if (postToDelete == null)
                return NotFound();

            await _postDal.Delete(postToDelete);

            if (await _postDal.SaveAll())
                return NoContent();

            return BadRequest("Catn erase the post");

        }


        [HttpPut("{postid}/update", Name = "Update Post")]
        public async Task<IActionResult> UpdatePost(Guid postid, [FromBody] PostForUpdateDto postForUpdateDto)
        {
            var postToUpdateFromRepo = (await _postDal.GetById(postid));

            var postTagsFromRepo = (await _postTagDal.ListAsync()).Where(pt => pt.PostId == postid).ToList();

            if (postToUpdateFromRepo == null)
                return NotFound();

            _mapper.Map(postForUpdateDto, postToUpdateFromRepo);

            if (await _postDal.SaveAll())
                return NoContent();

            return BadRequest("cant update the post!");
        }

        [HttpGet("{postid}", Name = "Get Single Post")]
        public async Task<IActionResult> GetPost(Guid postid)
        {
            var singlePostFromRepo = (await _postDal.GetById(postid));

            if (singlePostFromRepo == null)
                return NotFound();

            return Ok(singlePostFromRepo);

        }

        [HttpGet("{postId}/comments")]
        public async Task<IActionResult> GetComments(Guid postId)
        {
            var result = _postDal.ListAll()
            .SelectMany(p => _commentDal.ListAll(), (p, c) => new
            {
                Post = p,
                Comment = c
            }).Where(col => col.Post.Id == col.Comment.PostId).ToList();

            return Ok(result);
        }


    }
}