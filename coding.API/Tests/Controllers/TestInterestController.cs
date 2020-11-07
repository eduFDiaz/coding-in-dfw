using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.Interests;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace coding.API.Tests.Controllers
{
    public class TestInterestController
    {
         private readonly IMapper _mapper;
        Mock<IRepository<Interest>> mockRepo;
        Mock<IMapper> mockMapper;
        InterestController awardController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testInterestId;

        List<Interest> listInterest;

        Interest testInterest;

        UpdateInterestDto update;
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<UpdateInterestDto, Interest>();
                mc.CreateMap<CreateInterestDto, Interest>();
               
            });
        }
        // Init test
        public TestInterestController() {
         
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<Interest>>();

            mockMapper = new Mock<IMapper>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid();
            testInterestId = new Guid();

            listInterest = new List<Interest>() {
                new Interest() { Title = "Title", UserId = testUserId},
                new Interest() { Title = "Title 2", UserId = testUserId} 
                
            };

            testInterest = new Interest()
            {
                Id = testInterestId,
                Title = "Programming",
                UserId = testUserId
                                
            };

            update = new UpdateInterestDto() { Title = "Updated Title" };

            mockRepo.Setup(repo => repo.Add(testInterest)).ReturnsAsync(testInterest);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listInterest).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listInterest);
            mockRepo.Setup(repo => repo.GetById(testUserId)).ReturnsAsync(testInterest);
            mockRepo.Setup(repo => repo.Delete(testInterest)).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(testInterest)).ReturnsAsync(true);

            awardController = new InterestController(mockRepo.Object, mockConfiguration.Object, _mapper);
            
        }

        [Fact]
        public void InterestController_Returns_GetById()
        {
            // Act
            var okResult = awardController.GetinterestForUser(testUserId).Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

            var items = Assert.IsType<List<Interest>>(okResult.Value);

            Assert.Equal("Title", items[0].Title);
            Assert.Equal(testUserId, items[1].UserId);
            Assert.Equal(2, items.Count);
            
        }

        
        [Fact]
        public void Can_create_new_Interest()
        {
            // Given
            var newInterest = new CreateInterestDto() {
                Title = "New Interest",
                UserId = testUserId,
            };

            // Act
            var result = awardController.Create(newInterest).Result as OkObjectResult;

            // Assert
            Assert.IsType<InterestPresenter>(result.Value);
                      
        }

        [Fact]
        public async Task Can_delete_an_Interest()
        {
            // Act
            var result = await awardController.DeleteLan(testInterestId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Can_update_an_interest() {
            // Given
            var interestToUpdate = mockRepo.Object.GetById(testInterestId);
        
        
            // Act
            var result = await awardController.UpdateInterest(interestToUpdate.Result.Id, update) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

    }
}