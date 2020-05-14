using System;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.Posts;
using coding.API.Models.PostTags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using System.Text;



namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly Repository<Post> _postDal;
        private readonly Repository<PostTag> _postTagDal;
        private readonly IMapper _mapper;

        public TestController(IMapper mapper, IConfiguration config, Repository<Post> postDal, Repository<PostTag> postTagDal)
        {
            _config = config;
            _postDal = postDal;
            _postTagDal = postTagDal;
            _mapper = mapper;
            

        }

        [HttpGet("/test/")]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }

                
       
    }
}