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
using coding.API.Helpers;

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
        private readonly Repository<Comment> _commentDal;
        string password = "p@SSword";
        public PostController(
        Repository<Comment> commentDal,
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
            _commentDal = commentDal;
            _config = config;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PostForCreateDto request)
        {

            var postForCreate = _mapper.Map<Post>(request);
            var str = postForCreate.Text;
            
            var strEncryptred = Cipher.Encrypt(str, password);
            postForCreate.Text = strEncryptred;
            


            var createdPost = await _postDal.Add(postForCreate);

            foreach (var tag in request.PostTagId)
            {

                PostTag postag = new PostTag
                {
                    TagId = tag,
                    PostId = createdPost.Id, 
                    Tag = await _tagDal.GetById(tag)

                };


                await _postTagDal.Add(postag);
            }



            return Ok(new NewPostPresenter(createdPost));

        }


        [Authorize]
        [HttpGet("foruser/{userId}", Name = "GetPost")]
        public async Task<IActionResult> GetAllPostsForUser(Guid userId)
        {

            var allUserPosts = (await _postDal.GetRelatedFields("PostTags.Tag", "Comments")).Where(p => p.UserId == userId).ToList();
            

            var alluserPostsImages = (await _postDal.GetRelatedField("Photos")).Where(p => p.UserId == userId).ToList();

            List<PostPresenter> presentedPosts = new List<PostPresenter>();

            foreach (var post in allUserPosts)
            {   var text = post.Text;
                post.Text = Cipher.Decrypt(text,password); 
                presentedPosts.Add(new PostPresenter(post));
            }

            return Ok(presentedPosts);
        }

        [Authorize]
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

        [Authorize]
        [HttpPut("{postid}/update", Name = "Update Post")]
        public async Task<IActionResult> UpdatePost(Guid postid, [FromBody] PostForUpdateDto request)
        {
            var postToUpdate = (await _postDal.GetRelatedField("PostTags.Tag")).FirstOrDefault(p => p.Id == postid);
            var text = request.Text;
            var pt = new PostTagForCreateDto();
            var toUpd = _mapper.Map(request, postToUpdate);
            toUpd.Text = Cipher.Encrypt(text,password);
            foreach (var row in postToUpdate.PostTags)
            {

                var record = _postTagDal.GetRelatedRowPT(postid, row.TagId);
                if (record != null)
                {
                    _postTagDal.DeleteSync(record);

                }
            }

            foreach (var Tag in request.Tags)
            {

                pt.TagId = Tag;

                pt.PostId = toUpd.Id;

                pt.Tag = await _tagDal.GetById(Tag);

                var postTagToUpdate = _mapper.Map<PostTag>(pt);

                await _postTagDal.Add(postTagToUpdate);

            }

            var outPut = _mapper.Map<PostForDetailDto>(toUpd);
            outPut.Text = Cipher.Decrypt(outPut.Text,password);
            if (await _postDal.Update(toUpd))
                return Ok(outPut);

            return BadRequest("Cant update the post");

        }
        [Authorize]
        [HttpGet("{postid}", Name = "Get Single Post")]
        public async Task<IActionResult> GetPost(Guid postid)
        {
            var singlePostFromRepo = (await _postDal.GetRelatedField("PostTags.Tag")).SingleOrDefault(p => p.Id == postid);
            var text = singlePostFromRepo.Text;
            singlePostFromRepo.Text = Cipher.Decrypt(text,password);

            singlePostFromRepo.Comments = (await _commentDal.ListAsync()).Where(c => c.PostId == postid && c.Published == true).ToList();

            singlePostFromRepo.Photos = (await _postPhotoDal.ListAsync()).Where(p => p.PostId == postid).ToList();

            if (singlePostFromRepo == null)
                return NotFound();

            var outPut = _mapper.Map<PostAllCommentDetailDto>(singlePostFromRepo);

            return Ok(new PostPresenter(singlePostFromRepo));

        }
    }
}