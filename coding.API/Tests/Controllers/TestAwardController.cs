using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.Awards;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace coding.API.Tests
{
    public class TestAwardController
    {
        private readonly IMapper _mapper;
        Mock<IRepository<Award>> mockRepo;
        Mock<IMapper> mockMapper;
        AwardController awardController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testAwardId;

        List<Award> listAwards;

        Award testAward;

        UpdateAwardDto update;
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<UpdateAwardDto, Award>();
                mc.CreateMap<CreateAwardDto, Award>();
               
            });
        }
        // Init test
        public TestAwardController()
        {
         
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<Award>>();

            mockMapper = new Mock<IMapper>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid();
            testAwardId = new Guid();

            listAwards = new List<Award>() {
                new Award() { Company = "test", UserId = testUserId, Year = 2020},
                new Award() { Company = "Test2", UserId = testUserId, Year = 2021 }
                };

            testAward = new Award()
            {
                Id = testAwardId,
                Company = "Award",
                UserId = testUserId,
                Year = 2020
                
            };

            update = new UpdateAwardDto() {
                Company = "Updated Company",
                Title = "Updated Year",
                Year = 2021
            };

            mockRepo.Setup(repo => repo.Add(testAward)).ReturnsAsync(testAward);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listAwards).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listAwards);
            mockRepo.Setup(repo => repo.GetById(testUserId)).ReturnsAsync(testAward);
            mockRepo.Setup(repo => repo.Delete(testAward)).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(testAward)).ReturnsAsync(true);

            awardController = new AwardController(mockRepo.Object, _mapper, mockConfiguration.Object);
            
        }

        [Fact]
        public void AwardController_Returns_GetById()
        {
            // Act
            var okResult = awardController.GetawardForUser(testUserId).Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

            var items = Assert.IsType<List<Award>>(okResult.Value);

            Assert.Equal(2020, items[0].Year);
            Assert.Equal(testUserId, items[1].UserId);
            
        }

        [Fact]
        public void Check_AwardList_Returned()
        {
            // Act
            var okResult = awardController.GetawardForUser(testUserId).Result as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Award>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void Can_create_new_award()
        {
            // Given
            var newAward = new CreateAwardDto() {
                Company = "New Company",
                Title = "New AWard",
                UserId = testUserId,
                Year = 2020
            };

            // Act
            var result = awardController.Create(newAward).Result as OkObjectResult;

            // Assert
            Assert.IsType<AwardPresenter>(result.Value);
                      
        }

        [Fact]
        public async Task Can_delete_an_award()
        {
            // Act
            var result = await awardController.DeleteAward(testAwardId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Can_update_an_award() {
            // Given
            var awardToUpdate = mockRepo.Object.GetById(testAwardId);
        
        
            // Act
            var result = await awardController.UpdateAward(awardToUpdate.Result.Id, update) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

     }

    
}