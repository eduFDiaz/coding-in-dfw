using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.Users;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;



namespace coding.API.Tests
{
    public class TestAuthController
    {
        private readonly IMapper _mapper;
        Mock<IRepository<User>> mockRepo;
        Mock<IMapper> mockMapper;
        AuthController authController;
        Mock<IConfiguration> mockConfiguration;
        

        Guid testUserId;

        User testUser;

        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<UserForRegisterDto, User>();
                mc.CreateMap<UserForLoginDto, User>();
               
            });
        }
        // Init test
        public TestAuthController()
        {

            
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<User>>();

            mockMapper = new Mock<IMapper>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid();

            byte[] passwordHash, passwordSalt;

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                // passwordSalt is the key to verify the password when the user logs
                passwordSalt = hmac.Key;
                // password hash is just the computed hash of the password using the HMACSHA512 algorithm
                 passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Password"));
            }

            testUser = new User()
            {
                Id = testUserId,
                CodepenProfile = "Code Pen Profile",
                CustomUserTitle = "Custom test title",
                Email = "testuser@testdomain.com",
                FacebookProfile = "Facebook Profile",
                FullName = "John Doe",
                FullResume = "This is a full resume faked",
                GithubUrl = "Github url",
                LinkedInProfile = "Linkedin Profile",
                Location = "Coding in DFW",
                Phone = "5565644231",
                RedditProfile = "Reddit profile",
                Username = "test username",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
                
                
               
            };

            var mockIConfigurationSection = new Mock<IConfigurationSection>();
            mockIConfigurationSection.Setup(x => x.Path).Returns("AppSettings");
            mockIConfigurationSection.Setup(x => x.Key).Returns("Token");
            mockIConfigurationSection.Setup(x => x.Value).Returns("sjbkdfsjkfjknbfjbgijbrsjorjoppkoeokpaejpegarjop");
            mockConfiguration.Setup(config => config.GetSection(It.IsAny<String>())).Returns(mockIConfigurationSection.Object);

            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(new List<User>() {
                testUser
            });
            
            authController = new AuthController(_mapper, mockConfiguration.Object, mockRepo.Object);
            
        }

        [Fact]
        public void Can_register_a_user()
        {
            var userToRegister = new UserForRegisterDto() {
                Username = "testUser",
                Password = "MyP@ssWord",
                Email = "newuser@gmail.com"
            };

            // Act
            var okResult = authController.Register(userToRegister).Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

            var items = Assert.IsType<UserPresenter>(okResult.Value);

            
        }

        [Fact]
        public async Task Can_login_into_CodingInDfw() {
            // Assemble
            var userToLogIn = new UserForLoginDto() {
                Email = "testuser@testdomain.com",
                Password = "Password",
                Username = "Testusername"
            };

            // Act
            var result = await authController.Login(userToLogIn) as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);

        }

         [Fact]
        public async Task Cant_login_into_CodingInDfw_whit_a_false_password() {
           
            //Assemble         
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(new List<User>() {
                testUser
            });

            var userToLogIn = new UserForLoginDto() {
                Email = "testuser@testdomain.com",
                Password = "WrongPassword",
                Username = "Testusername"
            };

            // Act
            var result = await authController.Login(userToLogIn) as UnauthorizedResult;

            // Assert
            Assert.IsType<UnauthorizedResult>(result);

        }

     }
    
}