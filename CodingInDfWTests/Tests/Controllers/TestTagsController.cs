using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Models.PostTags;
using coding.API.Models.Presenter;
using coding.API.Models.Tags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;


namespace coding.API.Tests
{
    public class TestTagsController
    {
        private readonly IMapper _mapper;
        Mock<IRepository<Tag>> mockRepo;

        Mock<IRepository<PostTag>> mockPostTag;
           
        TagController TagController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testTagId;

        List<Tag> listTags;

        
        
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<TagForUpdateDto, Tag>();
                mc.CreateMap<TagForCreateDto, Tag>();
                               
            });
        }
        // Init test
        public TestTagsController()
        {

            
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<Tag>>();

            mockConfiguration = new Mock<IConfiguration>();

            mockPostTag = new Mock<IRepository<PostTag>>();

            testUserId = new Guid("968258bd-7198-464f-855e-18604fc1f870");
            testTagId = new Guid("23de061b-cb8e-46c1-b691-cd354fa1216b");

            listTags = new List<Tag>() {
                new Tag() { 
                    Id = testTagId,
                    Title = "Tag"
                }
            };

            mockPostTag.Setup(repo => repo.Add(It.IsAny<PostTag>())).ReturnsAsync(new PostTag());
            
            mockRepo.Setup(repo => repo.Add(It.IsAny<Tag>())).ReturnsAsync(listTags[0]);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listTags).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listTags);
            mockRepo.Setup(repo => repo.GetRelatedFields(It.IsAny<String>(), It.IsAny<String>())).ReturnsAsync(listTags);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(listTags[0]);
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Tag>())).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(It.IsAny<Tag>())).ReturnsAsync(true);

            TagController = new TagController(mockPostTag.Object, mockRepo.Object, mockConfiguration.Object, _mapper);
            
        }

        [Fact]
        public void TagController_Returns_AllTags()
        {
            // Act
            var okResult = TagController.GetAllTags().Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

            var items = Assert.IsType<List<Tag>>(okResult.Value);

            Assert.Equal("Tag", items[0].Title);
            
        }

        [Fact]
        public void Can_create_new_Tag()
        {
            // Given
            var newTag = new TagForCreateDto() {
                Title = "new Tag"
            };

            var result = TagController.Create(newTag).Result as OkObjectResult;

            // Assert
            Assert.IsType<TagPresenter>(result.Value);
                      
        }

        [Fact]
        public async Task Can_delete_an_Tag()
        {
            // Act
            var result = await TagController.DeleteTag(testTagId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Can_update_an_Tag() {
            // Assemble
            var TagToUpdate = mockRepo.Object.GetById(testTagId);

            var update = new TagForUpdateDto() {
              Title = "Updated Title"
            };
        
        
            // Act
            var result = await TagController.UpdateTag(TagToUpdate.Result.Id, update) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

         [Fact]
        public async Task Cant_delete_an_non_existing_item()
        {
            // Returning null
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as Tag);
            // Act
            var result = await TagController.DeleteTag(testTagId) as NotFoundResult;
            // Assert
            Assert.IsType<NotFoundResult>(result);        
            
        }

        [Fact]
        public async Task Cant_delete_an_existing_item_when_dbOperation_fails()
        {
            // Assemble to fail when deleting Tag record
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Tag>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Tag());

            // Act
            var result = await TagController.DeleteTag(testTagId) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_inexistent_item()
        {
            // Mock inexistent Tag
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(null as Tag);

            // Act
            var result = await TagController.UpdateTag(testTagId, new TagForUpdateDto()) as NotFoundObjectResult;

            // Assert
            Assert.IsNotType<NoContentResult>(result);

            Assert.IsType<NotFoundObjectResult>(result);

        }

        [Fact]
        public async Task Cant_update_an_item_when_db_query_fails()
        {
            // Mock the things
            mockRepo.Setup(repo => repo.Update(It.IsAny<Tag>())).ReturnsAsync(false);
            
            // Act
            var result = await TagController.UpdateTag(testTagId, new TagForUpdateDto()) as BadRequestObjectResult;

            // Assert it fails
            Assert.IsType<BadRequestObjectResult>(result);

        }

     }

    
}