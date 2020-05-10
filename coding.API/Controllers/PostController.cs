using coding.API.Models;
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
using coding.API.Models.Entities.Posts;
using coding.API.Models.Entities.PostTags;

using coding.API.Models.Interfaces;


namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController: ControllerBase
    {
        private readonly IPostRepo _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public PostController(IPostRepo repo, IConfiguration config, IMapper mapper)
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
        }

        
        [HttpPost("create")]
        public async Task<Post> Create([FromBody] PostForCreateDto postForCreateDto)
        {   

            // Creo una nueva instancia de PostTag asociandola con un Dto.
            var postTag = new PostTagForCreateDto();

            //Lo mismo pero con el post que voy a crear
            var postToCreate = _mapper.Map<Post>(postForCreateDto);

            //Creo el post (sin los tags ni nada)
            var createdPost = await _repo.Create(postToCreate);

            // Itero por los ids que recibo del usuario
            foreach (var Tag in postForCreateDto.TagId)
            {
                // Asigno el TagId
                postTag.TagId = Tag;
                // Asigno el PostId
                postTag.PostId = createdPost.Id;
                // Mapeo a postTag
                var postTagToCreate = _mapper.Map<PostTag>(postTag);
                // Guardo
                await _repo.AddTagsForPost(postTagToCreate);
            }
            //Guardo todo
            await _repo.SaveAll();

            // var postToReturn = _mapper.Map<PostForDetailDto>(createdPost);
            //Retorno el post que recien se creo.                   
            return createdPost;

        }

        
        [HttpGet("foruser/{id}", Name = "GetPost")]
        public async Task<IActionResult> GetAllUserPost(int id)
        {
            var postFromRepo = await _repo.GetAllUserPost(id);

            var postsToReturn = _mapper.Map<List<PostForDetailDto>>(postFromRepo);

            var postsSize = postFromRepo.Count;

            if (postsSize == 0)
                return NotFound();

            return Ok(postsToReturn);
        }

        [Authorize]
        [HttpDelete("{postid}/delete", Name = "DetelePost")]
        public async Task<IActionResult> DeletePost(int postid)
        {
                           
            var postFromRepo = await _repo.DeletePost(postid);

            if (!postFromRepo)
                return NotFound();
    
            return NoContent();
                        
        }

        [HttpPut("{postid}/update", Name = "Update Post")]
        public async Task<IActionResult> UpdatePost(int postid, [FromBody] PostForUpdateDto postForUpdateDto)
        {
          var postToUpdateFromRepo = await _repo.GetPost(postid);

        //   var postTag = new PostTagForCreateDto();
            
            if (postToUpdateFromRepo == null)
                return NotFound();
              
            var tagsForUpdate = await _repo.GetTagsForPost(postToUpdateFromRepo.Id);           

            // foreach (var tag in postForUpdateDto.TagId)
            // {
                 
            //     tagsForUpdate.TagId = tag;
            //     // Asigno el PostId
            //     // postToUpdateFromRepo.PostId = createdPost.Id;
            //     // Mapeo a postTag
            //     // var postTagToCreate = _mapper.Map<PostTag>(postTag);
            //     // Guardo
            //     await _repo.UpdateTagsForPost(tagsForUpdate);
            // }

            _mapper.Map(postForUpdateDto, postToUpdateFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Failed update");
        }

         [HttpGet("{postid}", Name = "Get Single Post")]
         public async Task<IActionResult> GetPost(int postid)
         {
             var postFromRepo = await _repo.GetPost(postid);

             if (postFromRepo == null)
                return NotFound();

            var postToReturn = _mapper.Map<PostForDetailDto>(postFromRepo);

            return Ok(postToReturn);
            
         }

        
    }
}