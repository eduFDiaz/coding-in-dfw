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


        
        [HttpGet("forpost/{postId}", Name = "Get skill for User")]
        public async Task<IActionResult> GetskillForUser(Guid postId)
        {

            var allPostComments = (await _commentDal.ListAsync()).Where(p => p.PostId == postId).ToList();

             return Ok(allPostComments);
        
        }

        
        [HttpDelete("{commentId}/delete", Name = "DeleteComment")]
        public async Task<IActionResult> DeleteLan(Guid commentId)
        {
           var commentToDelete = (await _commentDal.GetById(commentId));

             if (commentToDelete == null)
                  return NotFound();

            await _commentDal.Delete(commentToDelete);
                
            if (await _commentDal.SaveAll())    
                 return NoContent();

            return BadRequest("Catn erase the Comment");
            

        }
      
    }
}