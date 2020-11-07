using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.FAQS;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace coding.API.Tests
{
    public class TestFAQController
    {
        private readonly IMapper _mapper;
        Mock<IRepository<FAQ>> mockRepo;
        Mock<IMapper> mockMapper;
        FAQController faqController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testFAQId;

        List<FAQ> listFAQ;

        FAQ testFAQ;
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<UpdateFAQDto, FAQ>();
                mc.CreateMap<CreateFAQDto, FAQ>();
                mc.CreateMap<FAQ, FAQForDetailDto>();
               
            });
        }
        // Init test
        public TestFAQController()
        {
         
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<FAQ>>();

            mockMapper = new Mock<IMapper>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid();
            testFAQId = new Guid();

            listFAQ = new List<FAQ>() {
                new FAQ() { Description = "Test Description" ,Title = "Test Title",  UserId = testUserId},
                new FAQ() { Description = "Test Description 1", Title = "Test Title 1", UserId = testUserId }
                };

            testFAQ = new FAQ()
            {
                Id = testFAQId,
                Description = "Test",
                Title = "Test Title",
                UserId = testUserId
            };

            mockRepo.Setup(repo => repo.Add(testFAQ)).ReturnsAsync(testFAQ);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listFAQ).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listFAQ);
            mockRepo.Setup(repo => repo.GetById(testUserId)).ReturnsAsync(testFAQ);
            mockRepo.Setup(repo => repo.Delete(testFAQ)).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(testFAQ)).ReturnsAsync(true);

            faqController = new FAQController(mockRepo.Object, mockConfiguration.Object, _mapper);
            
        }

        [Fact]
        public void FAQController_Returns_AllFaqs()
        {
            // Act
            var okResult = faqController.GetFAQs();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            
        }

        [Fact]
        public void Check_FAQList_Returned()
        {
            // Act
            var okResult = faqController.GetFAQs().Result as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<FAQForDetailDto>>(okResult.Value);
            Assert.Equal(2, items.Count);

            
        }

        [Fact]
        public void Can_create_new_FAQ()
        {
            // Given
            var newFAQ = new CreateFAQDto() {
                Description = "New Description",
                Title = "New Title",
                UserId = testUserId,
                
            };

            // Act
            var result = faqController.Create(newFAQ).Result as OkObjectResult;
            
            // Assert
            Assert.IsType<FAQPresenter>(result.Value);
                       
        }

        [Fact]
        public async Task Can_delete_an_FAQ()
        {
            // Act
            var result = await faqController.DeleteLan(testFAQId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Can_update_an_FAQ() {
            // Given
            var faqToUpdate = mockRepo.Object.GetById(testFAQId);
        
            var update = new UpdateFAQDto() {
                Description = "Updated SchoolName",
                Title = "Updated Title",
                
            };
            // Act
            var result = await faqController.UpdateLan(faqToUpdate.Result.Id, update) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

     }

    
}