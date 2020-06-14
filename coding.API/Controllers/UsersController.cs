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
using coding.API.Data;
using coding.API.Models.Users;
using coding.API.Models.Presenter;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly Repository<User> _userDal;

        public UsersController(Repository<User> userDal, IMapper mapper)
        {
            _mapper = mapper;
            _userDal = userDal;

        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetUsers()
        {
            var users = (await _userDal.ListAsync());

            var usersToReturn = _mapper.Map<List<UserForDetailedDto>>(users);

            return Ok(usersToReturn);
        }
        
        [Authorize]
        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<ActionResult<User>> GetUser(Guid userId)
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