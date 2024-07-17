using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.Presenter;
using coding.API.Models.Skills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;



namespace coding.API.Tests
{
    public class TestSkillsController
    {
        private readonly IMapper _mapper;
        Mock<IRepository<Skill>> mockRepo;
           
        SkillController SkillController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testSkillId;

        List<Skill> listSkills;

        
        
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<UpdateSkillDto, Skill>();
                mc.CreateMap<CreateSkillDto, Skill>();
                               
            });
        }
        // Init test
        public TestSkillsController()
        {

            
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<Skill>>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid("968258bd-7198-464f-855e-18604fc1f870");
            testSkillId = new Guid("23de061b-cb8e-46c1-b691-cd354fa1216b");

            listSkills = new List<Skill>() {
                new Skill() { 
                    Id = testSkillId,
                    UserId = testUserId,
                    Title = "Skill"
                }
            };

            
            mockRepo.Setup(repo => repo.Add(It.IsAny<Skill>())).ReturnsAsync(listSkills[0]);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listSkills).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listSkills);
            mockRepo.Setup(repo => repo.GetRelatedFields(It.IsAny<String>(), It.IsAny<String>())).ReturnsAsync(listSkills);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(listSkills[0]);
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Skill>())).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(It.IsAny<Skill>())).ReturnsAsync(true);



            SkillController = new SkillController(mockRepo.Object,mockConfiguration.Object, _mapper);
            
        }

        [Fact]
        public void SkillController_Returns_GetById()
        {
            // Act
            var okResult = SkillController.GetskillForUser(testUserId).Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

            var items = Assert.IsType<List<Skill>>(okResult.Value);
            
        }

        [Fact]
        public void Can_create_new_Skill()
        {
            // Given
            var newSkill = new CreateSkillDto() {
                UserId = testUserId,
                Title = "new Skill"
            };

            var result = SkillController.Create(newSkill).Result as OkObjectResult;

            // Assert
            Assert.IsType<SkillPresenter>(result.Value);
                      
        }

        [Fact]
        public async Task Can_delete_an_Skill()
        {
            // Act
            var result = await SkillController.DeleteSkill(testSkillId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Can_update_an_Skill() {
            // Assemble
            mockRepo.Setup(repo => repo.GetRelatedField(It.IsAny<String>())).ReturnsAsync(listSkills);
            var SkillToUpdate = mockRepo.Object.GetById(testSkillId);

            var update = new UpdateSkillDto() {
              Title = "Updated Title"
            };
        
        
            // Act
            var result = await SkillController.UpdateSkill(SkillToUpdate.Result.Id, update) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

         [Fact]
        public async Task Cant_delete_an_non_existing_item()
        {
            // Returning null
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as Skill);
            // Act
            var result = await SkillController.DeleteSkill(testSkillId) as NotFoundResult;
            // Assert
            Assert.IsType<NotFoundResult>(result);        
            
        }

        [Fact]
        public async Task Cant_delete_an_existing_item_when_dbOperation_fails()
        {
            // Assemble to fail when deleting a record
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Skill>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Skill());

            // Act
            var result = await SkillController.DeleteSkill(testSkillId) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_inexistent_item()
        {
            // Mock inexistent Skill
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as Skill);

            // Act
            var result = await SkillController.UpdateSkill(testSkillId, new UpdateSkillDto()) as NotFoundResult;

            // Assert
            Assert.IsNotType<NoContentResult>(result);

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_item_when_db_query_fails()
        {
            // Mock the things
            mockRepo.Setup(repo => repo.Update(It.IsAny<Skill>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Skill());

            // Act
            var result = await SkillController.UpdateSkill(testSkillId, new UpdateSkillDto()) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

     }

    
}