using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Dtos;
using coding.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using coding.API.Models.Interfaces;
using coding.API.Models.Entities.Users;


namespace coding.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IRepo _repo;
        private readonly IMapper _mapper;

        public UsersController(IRepo repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetUsers()
        {
            var users = await this._repo.GetUsers();
            var usersToReturn = _mapper.Map<List<UserForDetailedDto>>(users); 
            return Ok(usersToReturn);
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            var user = await this._repo.GetUser(userId);
            var userToReturn = _mapper.Map<UserForDetailedDto>(user);
            return Ok(userToReturn);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
                
            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Failed update");
        }

    }
}