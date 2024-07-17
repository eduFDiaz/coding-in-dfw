using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;



namespace coding.API.Tests
{
    public class TestUsersController
    {
        private readonly IMapper _mapper;
        Mock<IRepository<User>> mockRepo;
        Mock<IMapper> mockMapper;
        UsersController UsersController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        List<User> listUsers;

        User testUser;

        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<UserForUpdateDto, User>();
                mc.CreateMap<User, UserForDetailedDto>();
                               
            });
        }
        // Init test
        public TestUsersController()
        {

            
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<User>>();

            mockMapper = new Mock<IMapper>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid("347da84a-d3ad-45e7-b4ad-aa9db24f2682");
            

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
                Username = "test username"
               
            };

            UsersController = new UsersController(mockRepo.Object, _mapper);
            
        }

        [Fact]
        public void UsersController_Returns_AllUsers()
        {
            // Assemble
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(new List<User>() {
                new User() {
                    Email = "test@email.com"
                }
            });
            // Act
            var okResult = UsersController.GetUsers().Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            
        }

        [Fact]
        public void UsersController_Returns_A_Single_User()
        {
            // Assemble
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(testUser);
            // Act
            var okResult = UsersController.GetUser(testUserId).Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            
        }

        [Fact]
        public void Can_update_a_User()
        {
            // Assemble
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(testUser);
            mockRepo.Setup(repo => repo.Update(It.IsAny<User>())).ReturnsAsync(true);

            // Given
            var updateUser = new UserForUpdateDto() {
                Email = "newemail@domain.com"
            };

            // Act
            var result = UsersController.UpdateUser(testUserId, updateUser).Result as OkObjectResult;

            // Assert
            Assert.IsType<UserForDetailedDto>(result.Value);
                      
        }

         [Fact]
        public void Cant_update_a_inexistent_User()
        {
            // Assemble
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as User);
            
                // Act
            var result = UsersController.UpdateUser(testUserId, new UserForUpdateDto()).Result as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(result);
                      
        }


        [Fact]
        public async Task Cant_update_an_existing_User_when_dbOperation_fails()
        {
            // Assemble to fail when deleting User record
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(testUser);
            mockRepo.Setup(repo => repo.Update(It.IsAny<User>())).ReturnsAsync(false);

            // Act
            var result = await UsersController.UpdateUser(testUserId, new UserForUpdateDto()) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

     }
   
}