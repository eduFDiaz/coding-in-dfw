using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.Languages;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace coding.API.Tests.Controllers
{
    public class TestLanguageController
    {
         private readonly IMapper _mapper;
        Mock<IRepository<Language>> mockRepo;
        Mock<IMapper> mockMapper;
        LanguageController awardController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testLanguageId;

        List<Language> listLanguage;

        Language testLanguage;

        UpdateLanguageDto update;
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<UpdateLanguageDto, Language>();
                mc.CreateMap<CreateLanguageDto, Language>();
               
            });
        }
        // Init test
        public TestLanguageController() {
         
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<Language>>();

            mockMapper = new Mock<IMapper>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid();
            testLanguageId = new Guid();

            listLanguage = new List<Language>() {
                new Language() { Name = "Spanish" , UserId = testUserId},
                new Language() { Name = "English", UserId = testUserId} 
                
            };

            testLanguage = new Language()
            {
                Id = testLanguageId,
                Name = "Japanese",
                UserId = testUserId
                                
            };

            update = new UpdateLanguageDto() { Name = "Updated Language" };

            mockRepo.Setup(repo => repo.Add(testLanguage)).ReturnsAsync(testLanguage);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listLanguage).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listLanguage);
            mockRepo.Setup(repo => repo.GetById(testUserId)).ReturnsAsync(testLanguage);
            mockRepo.Setup(repo => repo.Delete(testLanguage)).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(testLanguage)).ReturnsAsync(true);

            awardController = new LanguageController(mockRepo.Object, mockConfiguration.Object, _mapper);
            
        }

        [Fact]
        public void LanguageController_Returns_GetById()
        {
            // Act
            var okResult = awardController.GetLanForUser(testUserId).Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

            var items = Assert.IsType<List<Language>>(okResult.Value);

            Assert.Equal("Spanish", items[0].Name);
            Assert.Equal(testUserId, items[1].UserId);
            Assert.Equal(2, items.Count);
            
        }

        
        [Fact]
        public void Can_create_new_Interest()
        {
            // Given
            var newLanguage = new CreateLanguageDto() {
                Name = "Klingon",
                UserId = testUserId,
            };

            // Act
            var result = awardController.Create(newLanguage).Result as OkObjectResult;

            // Assert
            Assert.IsType<LanguagePresenter>(result.Value);
                      
        }

        [Fact]
        public async Task Can_delete_an_Language()
        {
            // Act
            var result = await awardController.DeleteLan(testLanguageId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Can_update_an_Language() {
            // Given
            var langaugeToUpdate = mockRepo.Object.GetById(testLanguageId);
        
        
            // Act
            var result = await awardController.UpdateLan(langaugeToUpdate.Result.Id, update) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

    }
}