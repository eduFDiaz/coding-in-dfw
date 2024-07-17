using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.Messages;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace coding.API.Tests.Controllers
{
    public class TestMessagesController
    {
         private readonly IMapper _mapper;
        Mock<IRepository<Message>> mockRepo;
        Mock<IMapper> mockMapper;
        MessageController messageController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testMessageId;

        List<Message> listMessage;

        Message testMessage;

        UpdateMessageDto update;
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<UpdateMessageDto, Message>();
                mc.CreateMap<CreateMessageDto, Message>();
               
            });
        }
        // Init test
        public TestMessagesController() {
         
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<Message>>();

            mockMapper = new Mock<IMapper>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid();
            testMessageId = new Guid();

            listMessage = new List<Message>() {
                new Message() { Name = "Jhon" , Email = "jhon@gmail.com" , isRead = false, ServiceType = "Web Creations", Text = "Test Message Body"},
                new Message() { Name = "Dennis" , Email = "dennis@gmail.com" , isRead = false, ServiceType = "Collaboration", Text = "Test Message Body"}
                
            };

            testMessage = new Message()
            {
                Id = testMessageId,
                Name = "New Guy",
                Email = "new@gmail.com",
                isRead = false,
                Text = " Hello Eduardo, this is a test message"
                                
            };

            update = new UpdateMessageDto() { isRead = true };

            mockRepo.Setup(repo => repo.Add(testMessage)).ReturnsAsync(testMessage);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listMessage).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listMessage);
            mockRepo.Setup(repo => repo.GetById(testUserId)).ReturnsAsync(testMessage);
            mockRepo.Setup(repo => repo.Delete(testMessage)).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(testMessage)).ReturnsAsync(true);

            messageController = new MessageController(mockRepo.Object, mockConfiguration.Object, _mapper);
            
        }

        [Fact]
        public void MessageController_Returns_GetById()
        {
            // Act
            var okResult = messageController.GetAllMessages().Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

            var items = Assert.IsType<List<Message>>(okResult.Value);

            Assert.Equal("dennis@gmail.com", items[1].Email);
            Assert.Equal("Jhon", items[0].Name);
            Assert.Equal(2, items.Count);
            
        }

        
        [Fact]
        public void Can_create_new_Message()
        {
            // Given
            var newMessage = new CreateMessageDto() {
                Email = "starwars@gmail.com",
                Name = "R2D2",
                ServiceType = "Fix",
                Text = "Can u fix me"
            };

            // Act
            var result = messageController.Create(newMessage).Result as OkObjectResult;

            // Assert
            Assert.IsType<MessagePresenter>(result.Value);
                      
        }

        [Fact]
        public async Task Can_delete_an_Message()
        {
            // Act
            var result = await messageController.DeleteMessage(testMessageId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Can_update_an_Message() {
            // Given
            var langaugeToUpdate = mockRepo.Object.GetById(testMessageId);
        
        
            // Act
            var result = await messageController.UpdateMessage(langaugeToUpdate.Result.Id) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Cant_delete_an_non_existing_item()
        {
            // Returning null
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as Message);
            // Act
            var result = await messageController.DeleteMessage(testMessageId) as NotFoundResult;
            // Assert
            Assert.IsType<NotFoundResult>(result);        
            
        }

        [Fact]
        public async Task Cant_delete_an_existing_item_when_dbOperation_fails()
        {
            // Assemble to fail when deleting education record
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Message>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Message());

            // Act
            var result = await messageController.DeleteMessage(testMessageId) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_inexistent_item()
        {
            // Mock inexistent education
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as Message);

            // Act
            var result = await messageController.UpdateMessage(testMessageId) as NotFoundResult;

            // Assert
            Assert.IsNotType<NoContentResult>(result);

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_item_when_db_query_fails()
        {
            // Mock the things
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Message>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Message());

            // Act
            var result = await messageController.UpdateMessage(testMessageId) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }


    }
}