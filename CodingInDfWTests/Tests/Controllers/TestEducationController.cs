using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.Educations;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace coding.API.Tests
{
    public class TestEducationController
    {
        private readonly IMapper _mapper;
        Mock<IRepository<Education>> mockRepo;
        Mock<IMapper> mockMapper;
        EducationController educationController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testEducationId;

        List<Education> listEducation;

        Education testEducation;
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<UpdateEducationDto, Education>();
                mc.CreateMap<CreateEducationDto, Education>();
               
            });
        }
        // Init test
        public TestEducationController()
        {
         
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<Education>>();

            mockMapper = new Mock<IMapper>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid();
            testEducationId = new Guid();

            listEducation = new List<Education>() {
                new Education() { SchoolName = "Test School", Title = "Test Title",  UserId = testUserId},
                new Education() { SchoolName = "Test School 1", Title = "Test Title 1", UserId = testUserId }
                };

            testEducation = new Education()
            {
                Id = testEducationId,
                SchoolName = "Test School",
                Title = "Test Title",
                UserId = testUserId
            };

            mockRepo.Setup(repo => repo.Add(testEducation)).ReturnsAsync(testEducation);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listEducation).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listEducation);
            mockRepo.Setup(repo => repo.GetById(testUserId)).ReturnsAsync(testEducation);
            mockRepo.Setup(repo => repo.Delete(testEducation)).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(testEducation)).ReturnsAsync(true);

            educationController = new EducationController(mockRepo.Object, mockConfiguration.Object, _mapper);
            
        }

        [Fact]
        public void educationController_Returns_GetById()
        {
            // Act
            var okResult = educationController.GetEducationForUser(testUserId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            
        }

        [Fact]
        public void Check_EducationList_Returned()
        {
            // Act
            var okResult = educationController.GetEducationForUser(testUserId).Result as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Education>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void Can_create_new_education()
        {
            // Given
            var newEducation = new CreateEducationDto() {
                SchoolName = "New SchoolName",
                Title = "New Title",
                UserId = testUserId,
                
            };

            // Act
            var result = educationController.Create(newEducation).Result as OkObjectResult;
            
            // Assert
            Assert.IsType<EducationPresenter>(result.Value);
                       
        }

        [Fact]
        public async Task Can_delete_an_education()
        {
            // Act
            var result = await educationController.DeleteLan(testEducationId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Can_update_an_education() {
            // Given
            var educationToUpdate = mockRepo.Object.GetById(testEducationId);
        
            var update = new UpdateEducationDto() {
                SchoolName = "Updated SchoolName",
                Title = "Updated Title",
                
            };
            // Act
            var result = await educationController.UpdateLan(educationToUpdate.Result.Id, update) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Cant_delete_an_non_existing_item()
        {
            // Returning null
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as Education);
            // Act
            var result = await educationController.DeleteLan(testEducationId) as NotFoundResult;
            // Assert
            Assert.IsType<NotFoundResult>(result);        
            
        }

        [Fact]
        public async Task Cant_delete_an_existing_item_when_dbOperation_fails()
        {
            // Assemble to fail when deleting education record
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Education>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Education());

            // Act
            var result = await educationController.DeleteLan(testEducationId) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_inexistent_item()
        {
            // Mock inexistent education
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as Education);

            // Act
            var result = await educationController.UpdateLan(testEducationId, new UpdateEducationDto()) as NotFoundResult;

            // Assert
            Assert.IsNotType<NoContentResult>(result);

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_item_when_db_query_fails()
        {
            // Mock the things
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Education>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Education());

            // Act
            var result = await educationController.UpdateLan(testEducationId, new UpdateEducationDto()) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

     }

    
}