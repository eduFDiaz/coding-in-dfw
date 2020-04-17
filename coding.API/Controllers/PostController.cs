using coding.API.Models;
using System.Collections.Generic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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
            this._repo = repo;
            this._config = config;
            this._mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<Post> Create([FromBody] PostForCreateDto postForCreateDto)
        {

            var postToCreate = _mapper.Map<Post>(postForCreateDto);

            var createdPost = await _repo.Create(postToCreate);

            return createdPost;
            
        }

        [HttpGet("{id}", Name = "GetPost")]
        public async Task<List<Post>> GetPosts()
        {
            var post = await _repo.GetPost();

            // var post = _mapper.Map<List<PostForDetailDto>>(postFromRepo);

            // var postToReturn = _mapper.Map<PostForDetailDto>(post);

            return post;
        }
        
    }
}