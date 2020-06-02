using AutoMapper;
using coding.API.Data;
using coding.API.Dtos.Comments;
using coding.API.Models;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

using coding.API.Models.Posts.Comments;


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using coding.API.Dtos;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly Repository<Comment> _commentDal;

        public CommentController(
            Repository<Comment> commentDal, IConfiguration config, IMapper mapper)
        {

            _commentDal = commentDal;
            _config = config;
            _mapper = mapper;
        }

        // [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto request)
        {
            var commentToCreate = _mapper.Map<Comment>(request);

            var createdComment = await _commentDal.Add(commentToCreate);

            return Ok(new CommentPresenter(createdComment));

        }



        [HttpGet("forpost/{postId}", Name = "Get comment for Post")]
        public async Task<IActionResult> GetcommentForUser(Guid postId)
        {

            var allPostComments = (await _commentDal.ListAsync()).Where(p => p.PostId == postId).ToList();
            var outPut = _mapper.Map<List<CommentForDetailDto>>(allPostComments);

            return Ok(outPut);

        }

        [HttpGet("unpublished", Name = "Get all unpublished comments")]
        public async Task<IActionResult> GetsAllUnpublishedComments()
        {

            var unpublished = (await _commentDal.GetRelatedField("Post")).Where(c => c.Published == false).ToList();

            var outPut = _mapper.Map<List<CommentForDetailDto>>(unpublished);
            return Ok(outPut);

        }

        [HttpPut("{commentId}/publish")]
        public async Task<IActionResult> UpdateComment(Guid commentId)
        {
            var comment = (await _commentDal.GetById(commentId));

            comment.Published = true;

            if (await _commentDal.SaveAll())
                return NoContent();

            return BadRequest("Cant publish the comment");

        }

        [HttpDelete("{commentId}/delete", Name = "DeleteComment")]
        public async Task<IActionResult> DeleteLan(Guid commentId)
        {
            var commentToDelete = (await _commentDal.GetById(commentId));

            if (commentToDelete == null)
                return NotFound();

            if (await _commentDal.Delete(commentToDelete))
                return NoContent();

            return BadRequest("Cant erase the Comment");


        }

        [HttpGet("comments/all")]
        public async Task<ActionResult> GetAllComments()
        {
            var comments = (await _commentDal.ListAsync()).ToList();
            return Ok(comments);
        }


    }
}