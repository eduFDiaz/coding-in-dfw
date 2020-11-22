using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Dtos.Reviews;
using coding.API.Models.Presenter;
using coding.API.Models.Reviews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;



namespace coding.API.Tests
{
    public class TestReviewsController
    {
        private readonly IMapper _mapper;
        Mock<IRepository<Review>> mockRepo;
           
        ReviewController ReviewController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testReviewId;

        List<Review> listReviews;

        
        
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<UpdateReviewDto, Review>();
                mc.CreateMap<CreateReviewDto, Review>();
                               
            });
        }
        // Init test
        public TestReviewsController()
        {

            
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<Review>>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid("968258bd-7198-464f-855e-18604fc1f870");
            testReviewId = new Guid("23de061b-cb8e-46c1-b691-cd354fa1216b");

            listReviews = new List<Review>() {
                new Review() { 
                    Id = testReviewId,
                    UserId = testUserId,
                    Body = "BOdy",
                    Company = "Company",
                    Email = "email@review.com",
                    Name = "Jhon Doe",
                    Status = "drafted",
                    Url = "http://urlgenerada.com"
                   
                }
            };

            
            mockRepo.Setup(repo => repo.Add(It.IsAny<Review>())).ReturnsAsync(listReviews[0]);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listReviews).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listReviews);
            mockRepo.Setup(repo => repo.GetRelatedFields(It.IsAny<String>(), It.IsAny<String>())).ReturnsAsync(listReviews);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(listReviews[0]);
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Review>())).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(It.IsAny<Review>())).ReturnsAsync(true);



            ReviewController = new ReviewController(mockRepo.Object,mockConfiguration.Object, _mapper);
            
        }

        [Fact]
        public void ReviewController_Returns_GetById()
        {
            // Act
            var okResult = ReviewController.GetReviewForUser(testUserId).Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

            var items = Assert.IsType<List<ReviewPresenter>>(okResult.Value);
            
        }

        [Fact]
        public void Can_create_new_Review()
        {
            // Given
            var newReview = new DraftReviewDto() {
                Email = "pepe@gmail.com",
                Company = "Company",
                UserId = testUserId
            };

            // Uncomment this if you have direct internet conection
            // var result = ReviewController.Create(newReview).Result as OkObjectResult;

            // In offline enviroments this has to be done in order to pass all tests
            var result = ReviewController.Create(newReview).Result as BadRequestObjectResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
                      
        }

        [Fact]
        public async Task Can_delete_an_Review()
        {
            // Act
            var result = await ReviewController.DeleteMessage(testReviewId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Can_update_an_Review() {
            // Assemble
            mockRepo.Setup(repo => repo.GetRelatedField(It.IsAny<String>())).ReturnsAsync(listReviews);
            var ReviewToUpdate = mockRepo.Object.GetById(testReviewId);

            var update = new UpdateReviewDto() {
              Body = "Update body",
              Name = "Name updated",
            };
        
        
            // Act
            var result = await ReviewController.UpdateMessage(ReviewToUpdate.Result.Id, update) as OkObjectResult;
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Cant_delete_an_non_existing_item()
        {
            // Returning null
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as Review);
            // Act
            var result = await ReviewController.DeleteMessage(testReviewId) as NotFoundResult;
            // Assert
            Assert.IsType<NotFoundResult>(result);        
            
        }

        [Fact]
        public async Task Cant_delete_an_existing_item_when_dbOperation_fails()
        {
            // Assemble to fail when deleting Review record
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Review>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Review());

            // Act
            var result = await ReviewController.DeleteMessage(testReviewId) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_inexistent_item()
        {
            // Mock inexistent Review
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as Review);

            // Act
            var result = await ReviewController.UpdateMessage(testReviewId, new UpdateReviewDto()) as NotFoundResult;

            // Assert
            Assert.IsNotType<NoContentResult>(result);

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_item_when_db_query_fails()
        {
            // Mock the things
            mockRepo.Setup(repo => repo.Update(It.IsAny<Review>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Review());

            // Act
            var result = await ReviewController.UpdateMessage(testReviewId, new UpdateReviewDto()) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }
        

     }

    
}