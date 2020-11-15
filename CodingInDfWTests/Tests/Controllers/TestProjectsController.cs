using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;

using coding.API.Models.Presenter;

using coding.API.Models.Products.Requirements;
using coding.API.Models.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;



namespace coding.API.Tests
{
    public class TestProjectController
    {
        private readonly IMapper _mapper;
        Mock<IRepository<Project>> mockRepo;
           
        ProjectController ProjectController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testProjectId;

        List<Project> listProjects;

        
        UpdateProjectDto update;
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<UpdateProjectDto, Project>();
                mc.CreateMap<CreateProjectDto, Project>();
                               
            });
        }
        // Init test
        public TestProjectController()
        {

            
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<Project>>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid("968258bd-7198-464f-855e-18604fc1f870");
            testProjectId = new Guid("23de061b-cb8e-46c1-b691-cd354fa1216b");

            listProjects = new List<Project>() {
                new Project() { 
                    Id = testProjectId,
                    UserId = testUserId,
                    Title = "Test Title",
                    Resume = "Resume test",
                    Type = "Type test"
                   
                }
            };

            
            mockRepo.Setup(repo => repo.Add(It.IsAny<Project>())).ReturnsAsync(listProjects[0]);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listProjects).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listProjects);
            mockRepo.Setup(repo => repo.GetRelatedFields(It.IsAny<String>(), It.IsAny<String>())).ReturnsAsync(listProjects);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(listProjects[0]);
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Project>())).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(It.IsAny<Project>())).ReturnsAsync(true);



            ProjectController = new ProjectController(mockRepo.Object,mockConfiguration.Object, _mapper);
            
        }

        [Fact]
        public void ProjectController_Returns_GetById()
        {
            // Act
            var okResult = ProjectController.GetProjectForUser(testUserId).Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

            var items = Assert.IsType<List<Project>>(okResult.Value);
            
        }

        [Fact]
        public void Can_create_new_Project()
        {
            // Given
            var newProject = new CreateProjectDto() {
                UserId = testUserId,
                Resume = "Resume test",
                Title = "Test title",
                Type = "Test type"
                          
            };

            // Act
            var result = ProjectController.Create(newProject).Result as OkObjectResult;

            // Assert
            Assert.IsType<ProjectPresenter>(result.Value);
                      
        }

        [Fact]
        public async Task Can_delete_an_Project()
        {
            // Act
            var result = await ProjectController.DeleteLan(testProjectId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Can_update_an_Project() {
            // Assemble
            mockRepo.Setup(repo => repo.GetRelatedField(It.IsAny<String>())).ReturnsAsync(listProjects);
            var ProjectToUpdate = mockRepo.Object.GetById(testProjectId);

            update = new UpdateProjectDto() {
                Resume = "Updat eresume",
                Title = "Update title",
                Type = "Update title"
            };
        
        
            // Act
            var result = await ProjectController.UpdateLan(ProjectToUpdate.Result.Id, update) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

     }

    
}