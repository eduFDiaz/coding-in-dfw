using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos.Comments;
using coding.API.Models.Posts.Comments;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;



namespace coding.API.Tests
{
    public class TestCommentController
    {
        private readonly IMapper _mapper;
        Mock<IRepository<Comment>> mockRepo;
        Mock<IMapper> mockMapper;
        CommentController CommentController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testCommentId;

        List<Comment> listComments;

        Comment testComment;

        
        //Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                    mc.CreateMap<CreateCommentDto, Comment>();
                    mc.CreateMap<Comment, CreateCommentDto>();
            });
        }
        // Init test
        public TestCommentController()
        {

            
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<Comment>>();

            mockMapper = new Mock<IMapper>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid();
            testCommentId = new Guid();

            listComments = new List<Comment>() {
                new Comment() { Body = "Test Body", CommenterName = "Test Commenter", Email = "email@comment"
                , Id = new Guid("9770e98d-5f80-455a-8421-ff438926ece8"), Published = false},
                
                };

            
     
            mockRepo.Setup(repo => repo.Add(testComment)).ReturnsAsync(testComment);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listComments).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listComments);
            mockRepo.Setup(repo => repo.GetById(testUserId)).ReturnsAsync(testComment);
            mockRepo.Setup(repo => repo.Delete(testComment)).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(testComment)).ReturnsAsync(true);

            CommentController = new CommentController(mockRepo.Object, mockConfiguration.Object, _mapper);
            
        }

        [Fact]
        public void CommentController_Returns_AllComments()
        {
            // Act
            var okResult = CommentController.GetAllComments().Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

            var items = Assert.IsType<List<Comment>>(okResult.Value);

            Assert.Equal("Test Body", items[0].Body);
          
        }

        [Fact]
        public void Can_create_new_Comment()
        {
            // Given
            var newComment = new CreateCommentDto() {
                Body = "Test Body", CommenterName = "Test Commenter", Email = "email@comment"
                , Published = false
               
            };

            // Act
            var result = CommentController.Create(newComment).Result as OkObjectResult;

            // Assert
            Assert.IsType<CommentPresenter>(result.Value);
                      
        }

        [Fact]
        public async Task Can_delete_an_existing_Comment()
        {
            // Assemble
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Comment() {
                Id = new Guid("37ac697d-9440-4e2b-b001-5f4a6d62dc6b")
            });
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Comment>())).ReturnsAsync(true);

            // Act
            var result = await CommentController.DeleteComment(testCommentId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Cant_delete_an_non_existing_Comment()
        {
            // Returning null
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as Comment);
            // Act
            var result = await CommentController.DeleteComment(testCommentId) as NotFoundResult;
            // Assert
            Assert.IsType<NotFoundResult>(result);        
            
        }

        [Fact]
        public async Task Cant_delete_an_existing_Comment_when_dbOperation_fails()
        {
            // Assemble to fail when deleting Comment record
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Comment>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(listComments[0]);

            // Act
            var result = await CommentController.DeleteComment(testCommentId) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_inexistent_Comment()
        {
            // Mock inexistent Comment
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as Comment);

            // Act
            var result = await CommentController.UpdateComment(testCommentId) as NotFoundResult;

            // Assert
            Assert.IsNotType<NoContentResult>(result);

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_Comment_when_db_query_fails()
        {
            // Mock the things
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Comment>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(listComments[0]);

            // Act
            var result = await CommentController.UpdateComment(testCommentId) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task Can_update_an_Comment() {
            // Given
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Comment());
            mockRepo.Setup(repo => repo.SaveAll()).ReturnsAsync(true);
                
            // Act
            var result = await CommentController.UpdateComment(testCommentId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

     }

    
}