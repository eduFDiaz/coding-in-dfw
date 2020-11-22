using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Models.Subscribers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace coding.API.Tests.Controllers
{
    public class TestNewsletterController
    {
        Mock<IRepository<Subscriber>> mockRepo;
        NewsletterController newsletterController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testSubscriberId;

        List<Subscriber> listSubscriber;

        Subscriber testSubscriber;

        
        // Init test
        public TestNewsletterController() {
         
            mockRepo = new Mock<IRepository<Subscriber>>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid();
            testSubscriberId = new Guid();

            listSubscriber = new List<Subscriber>() {
                new Subscriber() { Email = "jhon@gmail.com" },
                new Subscriber() { Email = "doe@gmail.com" },
            };

            testSubscriber = new Subscriber() { Email = "new@gmail.com" };

            mockRepo.Setup(repo => repo.Add(testSubscriber)).ReturnsAsync(testSubscriber);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listSubscriber).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listSubscriber);
            mockRepo.Setup(repo => repo.GetById(testUserId)).ReturnsAsync(testSubscriber);
            mockRepo.Setup(repo => repo.Delete(testSubscriber)).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(testSubscriber)).ReturnsAsync(true);

            newsletterController = new NewsletterController(mockRepo.Object, mockConfiguration.Object);
            
        }

        [Fact]
        public void SubscriberController_Returns_GetById()
        {
            // Act
            var okResult = newsletterController.GetAllSubcriptions().Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

            var items = Assert.IsType<List<Subscriber>>(okResult.Value);

            Assert.Equal(2, items.Count);
            
        }

        
        [Fact]
        public void Can_create_new_Subscriber()
        {
            // Given
            var newSubscriber = new Subscriber() {
                Email = "lola@gmail.com"
            };

            // Act
            var result = newsletterController.Create(newSubscriber).Result as BadRequestObjectResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
                      
        }

        [Fact]
        public async Task Can_delete_an_Subscriber()
        {
            // Act
            var result = await newsletterController.DeleteMessage(testSubscriberId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Cant_delete_an_non_existing_item()
        {
            // Returning null
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as Subscriber);
            // Act
            var result = await newsletterController.DeleteMessage(testSubscriberId) as NotFoundResult;
            // Assert
            Assert.IsType<NotFoundResult>(result);        
            
        }

        [Fact]
        public async Task Cant_delete_an_existing_item_when_dbOperation_fails()
        {
            // Assemble to fail when deleting education record
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Subscriber>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Subscriber());

            // Act
            var result = await newsletterController.DeleteMessage(testSubscriberId) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }
    }
}