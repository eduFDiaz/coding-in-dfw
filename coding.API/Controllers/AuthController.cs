using System;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.Users;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;



namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IRepository<User> _userDal;
        private readonly IMapper _mapper;

        public AuthController(IMapper mapper, IConfiguration config, IRepository<User> userDal)
        {
            _config = config;
            _userDal = userDal;
            _mapper = mapper;
            

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            userForRegisterDto.Email = userForRegisterDto.Email.ToLower();

            // this should not me here
            byte[] passwordHash, passwordSalt;

            // only one user must be register, so this should return nothing
            var user = 
                (await _userDal.ListAsync())
                    .SingleOrDefault(u => u.Email == userForRegisterDto.Email);

            // if user were found, is already registered
            if (user != default)
            {
                ModelState.AddModelError(
                    "err_already_exists",
                    "User already register. Forgot password?");

                return BadRequest(ModelState);
            }

            // I would start a service named usermanager, handle things like this
            // as you can see, this kind of logic make «noise» in the controller
            // with a lot of code. You should call some service to do with the user
            // what you want to do. Generate password, change it, register, etc...
            // In a well done achitecture, you should not access to data layer in controller.
            //
            // Here is the logic to encrypt password 
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                // passwordSalt is the key to verify the password when the user logs
                passwordSalt = hmac.Key;
                // password hash is just the computed hash of the password using the HMACSHA512 algorithm
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userForRegisterDto.Password));
            }

            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            userToCreate.PasswordHash = passwordHash;
            userToCreate.PasswordSalt = passwordSalt;
         
            await _userDal.Add(userToCreate);

            return Ok(new UserPresenter(userToCreate));
       
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
             var userFromRepo = 
                (await _userDal.ListAsync())
                    .SingleOrDefault(u => u.Email == userForLoginDto.Email);

            if (userFromRepo == null)
                //return NotFound("Cant find that user!"); // don't tell the user what are you doing here
                return NotFound("Invalid login credentials.");

            // well, well... another method to you UserManagerService
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userFromRepo.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userForLoginDto.Password));

                // Verify that the password computedHash is equal to the stored passwordHash
                for (int i = 0; i < computedHash.Length; i++)
                    if (userFromRepo.PasswordHash[i] != computedHash[i])
                        return  Unauthorized();
                        
            }   
            
            // HUGE MISTAKE! use not anonymous out of a lambda expresion
            // better enough, use never anonymous or «object»
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username),
                new Claim(ClaimTypes.Email, userFromRepo.Email)
            };
           
            // // Get the secret key from the appsettings.json config file
            // // and create a Symmetric Security Key with it
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            // // Create a digital signature using the Symmetric Key using SHA512
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // // Body of the token
             var tokenDescriptor = new SecurityTokenDescriptor
             {
                 Subject = new ClaimsIdentity(claims),
                 Expires = DateTime.Now.AddDays(1),
                 SigningCredentials = credentials
             };

             var tokenHandler = new JwtSecurityTokenHandler();

            // // The token is created to be returned when the user logs in
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // // The just logged in user is passed to be consumed by auth service and so on
            // var user = _mapper.Map<UserForDetailedDto>(userFromRepo);
            var user = new UserPresenter(userFromRepo);

             return Ok(new
                 {
                 token = tokenHandler.WriteToken(token),
                 user
             });
            
        }
    }
}