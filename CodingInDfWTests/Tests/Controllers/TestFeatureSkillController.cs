using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.FeatureFeatureSkill;
using coding.API.Models.FeatureSkills;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace coding.API.Tests
{
    public class TestFeatureSkillController
    {
        private readonly IMapper _mapper;
        Mock<IRepository<FeatureSkill>> mockRepo;
        Mock<IMapper> mockMapper;
        FeatureSkillController featureSkillController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testfeatureSkillId;

        List<FeatureSkill> listfeatureFeatureSkill;

        FeatureSkill testfeatureSkill;

        UpdateFeatureSkillDto update;
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<UpdateFeatureSkillDto, FeatureSkill>();
                mc.CreateMap<CreateFeatureSkillDto, FeatureSkill>();
               
            });
        }
        // Init test
        public TestFeatureSkillController()
        {
         
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<FeatureSkill>>();

            mockMapper = new Mock<IMapper>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid();
            testfeatureSkillId = new Guid();

            string[] icons = new string[] { "Icon1", "icon 2" };
         
            listfeatureFeatureSkill = new List<FeatureSkill>() {
                new FeatureSkill() { Body = "test body ", Icons = icons ,Title = "Test Title" },
                new FeatureSkill() { Body = "test body 1 ", Icons = icons ,Title = "Test Title 1" },
                new FeatureSkill() { Body = "test body 3 ", Icons = icons ,Title = "Test Title 2" },
                
            };

            testfeatureSkill = new FeatureSkill()
            {
                Id = testfeatureSkillId,
                Icons = icons,
                Body = "Test Body"
                                       
            };

            update = new UpdateFeatureSkillDto() {
                Body = "Update body",
                Title = "Updated Year",
                Icons = icons
            };

            mockRepo.Setup(repo => repo.Add(testfeatureSkill)).ReturnsAsync(testfeatureSkill);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listfeatureFeatureSkill).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listfeatureFeatureSkill);
            mockRepo.Setup(repo => repo.GetById(testUserId)).ReturnsAsync(testfeatureSkill);
            mockRepo.Setup(repo => repo.Delete(testfeatureSkill)).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(testfeatureSkill)).ReturnsAsync(true);

            featureSkillController = new FeatureSkillController(mockRepo.Object, mockConfiguration.Object, _mapper);
            
        }

        [Fact]
        public void featureSkillController_Returns_GetById()
        {
            // Act
            var okResult = featureSkillController.GetAllFeatureFeatureSkill().Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
                       
        }

        [Fact]
        public void Can_create_new_featureSkill()
        {
            // Given
            var newfeatureSkill = new CreateFeatureSkillDto() {
                Body = "New Body",
                Title = "New featureSkill",
                Icons = new string[] { "icon1", "icon2", "icon3" }
                
            };

            // Act
            var result = featureSkillController.Create(newfeatureSkill).Result as OkObjectResult;

            // Assert
            Assert.IsType<FeatureSkillPresenter>(result.Value);
                      
        }

        [Fact]
        public async Task Can_delete_an_featureSkill()
        {
            // Act
            var result = await featureSkillController.DeleteFeatureSkill(testfeatureSkillId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Can_update_an_featureSkill() {
            // Given
            var featureSkillToUpdate = mockRepo.Object.GetById(testfeatureSkillId);
        
        
            // Act
            var result = await featureSkillController.UpdateFeatureSkill(featureSkillToUpdate.Result.Id, update) as OkObjectResult;
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

         [Fact]
        public async Task Cant_delete_an_non_existing_item()
        {
            // Returning null
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as FeatureSkill);
            // Act
            var result = await featureSkillController.DeleteFeatureSkill(testfeatureSkillId) as NotFoundResult;
            // Assert
            Assert.IsType<NotFoundResult>(result);        
            
        }

        [Fact]
        public async Task Cant_delete_an_existing_item_when_dbOperation_fails()
        {
            // Assemble to fail when deleting education record
            mockRepo.Setup(repo => repo.Delete(It.IsAny<FeatureSkill>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new FeatureSkill));

            // Act
            var result = await featureSkillController.DeleteFeatureSkill(testfeatureSkillId) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_inexistent_item()
        {
            // Mock inexistent education
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as Skill);

            // Act
            var result = await SkillController.UpdateLan(testSkillId, new UpdateSkillDto()) as NotFoundResult;

            // Assert
            Assert.IsNotType<NoContentResult>(result);

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_item_when_db_query_fails()
        {
            // Mock the things
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Skill>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Skill());

            // Act
            var result = await SkillController.UpdateLan(testSkillId, new UpdateSkillDto()) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

     }

    
}