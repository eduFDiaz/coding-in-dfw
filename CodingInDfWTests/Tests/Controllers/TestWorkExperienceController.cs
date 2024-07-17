using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.WorkExperiences;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;



namespace coding.API.Tests
{
    public class TestWorkExperienceController
    {
        private readonly IMapper _mapper;
        Mock<IRepository<WorkExperience>> mockRepo;
        Mock<IMapper> mockMapper;
        WorkExperienceController WorkExperienceController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testWorkExperienceId;

        List<WorkExperience> listWorkExperiences;

        WorkExperience testWorkExperience;

        UpdateWorkExperienceDto update;
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<UpdateWorkExperienceDto, WorkExperience>();
                mc.CreateMap<CreateWorkExperienceDto, WorkExperience>();
               
            });
        }
        // Init test
        public TestWorkExperienceController()
        {

            
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<WorkExperience>>();

            mockMapper = new Mock<IMapper>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid();
            testWorkExperienceId = new Guid();

            listWorkExperiences = new List<WorkExperience>() {
                new WorkExperience() { Company = "test", UserId = testUserId, Resume = "Test", Title = "Title" },
                new WorkExperience() { Company = "Test2", UserId = testUserId, Resume = "Test", Title = "Title" }
                };

            testWorkExperience = new WorkExperience()
            {
                Id = testWorkExperienceId,
                Company = "WorkExperience",
                UserId = testUserId
                                
            };

            update = new UpdateWorkExperienceDto() {
                Company = "Updated Company",
                Title = "Updated Year",
                
            };

            mockRepo.Setup(repo => repo.Add(testWorkExperience)).ReturnsAsync(testWorkExperience);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listWorkExperiences).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listWorkExperiences);
            mockRepo.Setup(repo => repo.GetById(testUserId)).ReturnsAsync(testWorkExperience);
            mockRepo.Setup(repo => repo.Delete(testWorkExperience)).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(testWorkExperience)).ReturnsAsync(true);

            WorkExperienceController = new WorkExperienceController(mockRepo.Object, mockConfiguration.Object,_mapper);
            
        }

        [Fact]
        public void WorkExperienceController_Returns_GetById()
        {
            // Act
            var okResult = WorkExperienceController.GetworkExperienceForUser(testUserId).Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

            var items = Assert.IsType<List<WorkExperience>>(okResult.Value);
            
        }

        [Fact]
        public void Can_create_new_WorkExperience()
        {
            // Given
            var newWorkExperience = new CreateWorkExperienceDto() {
                Company = "New Company",
                Title = "New WorkExperience",
                UserId = testUserId,
                
            };

            // Act
            var result = WorkExperienceController.Create(newWorkExperience).Result as OkObjectResult;

            // Assert
            Assert.IsType<WorkExperiencePresenter>(result.Value);
                      
        }

        [Fact]
        public async Task Can_delete_an_existing_WorkExperience()
        {
            // Act
            var result = await WorkExperienceController.DeleteWorkExperience(testWorkExperienceId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Cant_delete_an_non_existing_WorkExperience()
        {
            // Returning null
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as WorkExperience);
            // Act
            var result = await WorkExperienceController.DeleteWorkExperience(testWorkExperienceId) as NotFoundResult;
            // Assert
            Assert.IsType<NotFoundResult>(result);        
            
        }

        [Fact]
        public async Task Cant_delete_an_existing_WorkExperience_when_dbOperation_fails()
        {
            // Assemble to fail when deleting WorkExperience record
            mockRepo.Setup(repo => repo.Delete(It.IsAny<WorkExperience>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(listWorkExperiences[0]);

            // Act
            var result = await WorkExperienceController.DeleteWorkExperience(testWorkExperienceId) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_inexistent_WorkExperience()
        {
            // Mock inexistent WorkExperience
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as WorkExperience);

            // Act
            var result = await WorkExperienceController.UpdateworkExperience(testWorkExperienceId, new UpdateWorkExperienceDto()) as NotFoundResult;

            // Assert
            Assert.IsNotType<NoContentResult>(result);

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_WorkExperience_when_db_query_fails()
        {
            // Mock the things
            mockRepo.Setup(repo => repo.Delete(It.IsAny<WorkExperience>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(listWorkExperiences[0]);

            // Act
            var result = await WorkExperienceController.UpdateworkExperience(testWorkExperienceId, new UpdateWorkExperienceDto()) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task Can_update_an_WorkExperience() {
            // Given
            var WorkExperienceToUpdate = mockRepo.Object.GetById(testWorkExperienceId);
                
            // Act
            var result = await WorkExperienceController.UpdateworkExperience(WorkExperienceToUpdate.Result.Id, update) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

     }

    
}