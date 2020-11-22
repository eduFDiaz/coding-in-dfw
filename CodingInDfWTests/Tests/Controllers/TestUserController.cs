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

        // [Fact]
        // public void Check_UserList_Returned()
        // {
        //     // Act
        //     var okResult = UsersController.GetUserForUser(testUserId).Result as OkObjectResult;
        //     // Assert
        //     var items = Assert.IsType<List<User>>(okResult.Value);
        //     Assert.Equal(2, items.Count);
        // }

        // [Fact]
        // public void Can_create_new_User()
        // {
        //     // Given
        //     var newUser = new CreateUserDto() {
        //         Company = "New Company",
        //         Title = "New User",
        //         UserId = testUserId,
        //         Year = 2020
        //     };

        //     // Act
        //     var result = UsersController.Create(newUser).Result as OkObjectResult;

        //     // Assert
        //     Assert.IsType<UserPresenter>(result.Value);
                      
        // }

        // [Fact]
        // public async Task Can_delete_an_existing_User()
        // {
        //     // Act
        //     var result = await UsersController.DeleteUser(testUserId) as NoContentResult;
        //     // Assert
        //     Assert.IsType<NoContentResult>(result);        
            
        // }

        // [Fact]
        // public async Task Cant_delete_an_non_existing_User()
        // {
        //     // Returning null
        //     mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as User);
        //     // Act
        //     var result = await UsersController.DeleteUser(testUserId) as NotFoundResult;
        //     // Assert
        //     Assert.IsType<NotFoundResult>(result);        
            
        // }

        // [Fact]
        // public async Task Cant_delete_an_existing_User_when_dbOperation_fails()
        // {
        //     // Assemble to fail when deleting User record
        //     mockRepo.Setup(repo => repo.Delete(It.IsAny<User>())).ReturnsAsync(false);
        //     mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(listUsers[0]);

        //     // Act
        //     var result = await UsersController.DeleteUser(testUserId) as BadRequestObjectResult;

        //     // Assert it fails
        //     Assert.IsType<BadRequestObjectResult>(result);

        // }

        // [Fact]
        // public async Task Cant_update_an_inexistent_User()
        // {
        //     // Mock inexistent User
        //     mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as User);

        //     // Act
        //     var result = await UsersController.UpdateUser(testUserId, new UpdateUserDto()) as NotFoundResult;

        //     // Assert
        //     Assert.IsNotType<NoContentResult>(result);

        //     Assert.IsType<NotFoundResult>(result);

        // }

        // [Fact]
        // public async Task Cant_update_an_User_when_db_query_fails()
        // {
        //     // Mock the things
        //     mockRepo.Setup(repo => repo.Delete(It.IsAny<User>())).ReturnsAsync(false);
        //     mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(listUsers[0]);

        //     // Act
        //     var result = await UsersController.UpdateUser(testUserId, new UpdateUserDto()) as BadRequestObjectResult;

        //     // Assert it fails
        //     Assert.IsType<BadRequestObjectResult>(result);

        // }

        // [Fact]
        // public async Task Can_update_an_User() {
        //     // Given
        //     var UserToUpdate = mockRepo.Object.GetById(testUserId);
                
        //     // Act
        //     var result = await UsersController.UpdateUser(UserToUpdate.Result.Id, update) as NoContentResult;
        //     // Assert
        //     Assert.IsType<NoContentResult>(result);
        // }

     }

    
}