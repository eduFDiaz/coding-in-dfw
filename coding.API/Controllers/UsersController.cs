using System;
using System.Collections;
using System.Collections.Generic;

using System.Threading.Tasks;
using AutoMapper;
using coding.API.Dtos;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using coding.API.Data;
using coding.API.Models.Users;


namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IRepository<User> _userDal;

        public UsersController(IRepository<User> userDal, IMapper mapper)
        {
            _mapper = mapper;
            _userDal = userDal;

        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userDal.ListAsync();

            var usersToReturn = _mapper.Map<List<UserForDetailedDto>>(users);

            return Ok(usersToReturn);
        }
        
        [Authorize]
        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var user = await _userDal.GetById(userId);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);

        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserForUpdateDto userForUpdateDto)
        {
            var userFromRepo = await _userDal.GetById(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _userDal.Update(userFromRepo))
            {
                var output = _mapper.Map<UserForDetailedDto>(userFromRepo);
                return Ok(output);
            }

            return BadRequest("Cant update the user");
        }

    }
}