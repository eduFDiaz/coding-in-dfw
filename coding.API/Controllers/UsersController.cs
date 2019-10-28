using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coding.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IRepo _repo;

        public UsersController(IRepo repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetUsers()
        {
            var users = await this._repo.GetUsers();
            return Ok(users);
        }
        
    }
}