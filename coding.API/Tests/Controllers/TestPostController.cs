using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Dtos.Posts;
using coding.API.Helpers;
using coding.API.Models.Photos;
using coding.API.Models.Posts;
using coding.API.Models.Posts.Comments;
using coding.API.Models.PostTags;
using coding.API.Models.Presenter;
using coding.API.Models.Subscribers;
using coding.API.Models.Tags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace coding.API.Tests.Controllers
{
    public class TestPostController
    {
        private readonly IMapper _mapper;
        private Mock<IRepository<Post>> mockPostRepo;
        private Mock<ICollection<Guid>> mockICollection;
        private Mock<IMapper> mockMapper;
        private PostController postController;
        private Mock<IConfiguration> mockConfiguration;
        private Mock<IRepository<Tag>> mockTagRepo;
        private Mock<IRepository<PostTag>> mockPostTagRepo;
        private Mock<IRepository<Subscriber>> mockSubRepo;
        private Mock<IRepository<Comment>> mockCommentRepo;
        private Mock<IRepository<PostPhoto>> mockPostPhotoRepo;
        private Guid testUserId;

        private Guid testPostId;

        private List<Post> listPost;
        private Post testPost;
        private PostForUpdateDto update;
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<PostForUpdateDto, Post>();
                mc.CreateMap<PostForCreateDto, Post>();
                mc.CreateMap<Post, PostAllCommentDetailDto>();
               
            });
        }
        // Init test
        public TestPostController() {
         
            _mapper = new Mapper(CreateMaps());

            mockPostRepo = new Mock<IRepository<Post>>();

            mockICollection = new Mock<ICollection<Guid>>();

            mockMapper = new Mock<IMapper>();

            mockConfiguration = new Mock<IConfiguration>();

            mockTagRepo = new Mock<IRepository<Tag>>();

            mockPostTagRepo = new Mock<IRepository<PostTag>>();

            mockSubRepo = new Mock<IRepository<Subscriber>>();

            mockCommentRepo = new Mock<IRepository<Comment>>();

            mockPostPhotoRepo = new Mock<IRepository<PostPhoto>>();

            testUserId = new Guid("363ed01a-399e-4bb5-8515-5563a80abde4");

            testPostId = new Guid("bc0321a0-ba20-4c2b-b392-c23e569cbfd7");

            var strEncryptred = Cipher.Encrypt("Texto encriptado de prueba", "p@SSword");
            
            
                        
            // Assemble post List
            listPost = new List<Post>() {
                new Post() { Id = new Guid("c1ca3ab3-b390-4bb5-b9cc-08968d79987e"), UserId = testUserId , Description = "Test Description", ReadingTime = 12, Title = "Test Title", Text = strEncryptred.ToString() },
                new Post() { Id = new Guid("c0f7879e-3992-446a-a8ad-fcf82a2a7188"), UserId = testUserId , Description = "Test Description 1", ReadingTime = 12, Title = "Test Title 1", Text = strEncryptred.ToString()},
               
            };

            // Assemble single post to add
            testPost = new Post() {
                Text = "asdasdkjslasdjlasdjasd",
                UserId = testUserId
            };

            // Assemble post update
            update = new PostForUpdateDto() { Title = "Updated Post" };
            
            // Init Icollection mocking
            mockICollection.Setup(icol => icol.Add(new Guid("c398fd6b-5426-478d-9fac-5d69505d0cd0")));

            // Init Post Photo Repo
            mockPostPhotoRepo.Setup(postphoto => postphoto.Add(It.IsAny<PostPhoto>())).ReturnsAsync(new PostPhoto());
            mockPostPhotoRepo.Setup(postphoto => postphoto.ListAsync()).ReturnsAsync(new List<PostPhoto>() {
                new PostPhoto() {
                    IsMain = true,
                    PostId = new Guid("c0f7879e-3992-446a-a8ad-fcf82a2a7188"),
                    Id = new Guid("7649e53dc5d84b73bb35962ec01091c4")
                }    
            });

            // Init Tag Repo
            mockTagRepo.Setup(tag => tag.Add(It.IsAny<Tag>())).ReturnsAsync(new Tag() {
                Id = new Guid("74030366-488d-44e4-b889-25f200e782eb"),
                Title = "TestTag"
            });

            // Init Comment Repo
            mockCommentRepo.Setup(comment => comment.ListAsync()).ReturnsAsync(new List<Comment>() {
                new Comment() {
                    Id = new Guid("10fd3452-def3-47c4-b92d-bc6257e007d9"),
                    Body = "Nice post bro!",
                    CommenterName = "Test Commenter",
                    Email = "pancha@gmail.com",
                    Published = true,
                    PostId = new Guid("c0f7879e-3992-446a-a8ad-fcf82a2a7188")
                }
            });

            // Init subscriber repo
            mockSubRepo.Setup(sub => sub.Add(It.IsAny<Subscriber>())).ReturnsAsync(new Subscriber());

            // Init Post Repo
            mockPostRepo.Setup(post => post.Add(It.IsAny<Post>())).ReturnsAsync(new Post()
                {
                    Id = testPostId,
                    Description = "Test",
                    Text = "asdasdasdasdasdasd21342134234234",
                    UserId = testUserId,
                    ReadingTime = 2,
                    Title = "Test Title",
                    Comments = new List<Comment>() {
                        new Comment()
                    },
                    PostTags = new List<PostTag>() {
                        new PostTag() {
                            PostId = testPostId,

                        }
                    }
                }
            );

            mockPostRepo.Setup(repo => repo.ListAll()).Returns(listPost).Verifiable();
            mockPostRepo.Setup(repo => repo.GetRelatedFields("PostTags.Tag", "Comments")).ReturnsAsync(listPost);
            mockPostRepo.Setup(repo => repo.GetRelatedField("PostTags.Tag")).ReturnsAsync(listPost);
            mockPostRepo.Setup(repo => repo.GetRelatedField("Photos")).ReturnsAsync(listPost);
            mockPostRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listPost);
            mockPostRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Post());
            mockPostRepo.Setup(repo => repo.Delete(It.IsAny<Post>())).ReturnsAsync(true);
            mockPostRepo.Setup(repo => repo.Update(It.IsAny<Post>())).ReturnsAsync(true);

            postController = new PostController(mockCommentRepo.Object, 
            mockPostTagRepo.Object, mockPostPhotoRepo.Object, mockTagRepo.Object,
            mockPostRepo.Object, mockSubRepo.Object, mockConfiguration.Object, _mapper);
            
        }

        [Fact]
        public void PostController_Returns_AllPostsFromAUser()
        {
            // Act
            var okResult = postController.GetAllPostsForUser(testUserId).Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

            var items = Assert.IsType<List<PostPresenter>>(okResult.Value);

            Assert.Equal(new Guid("c1ca3ab3-b390-4bb5-b9cc-08968d79987e"), items[0].Id);
            Assert.Equal(12, items[1].ReadingTime);

                
        }

        [Fact]
        public async Task Can_get_single_post() {
            var okResult = await postController.GetPost(new Guid("c1ca3ab3-b390-4bb5-b9cc-08968d79987e")) as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult);

            var post = Assert.IsType<PostPresenter>(okResult.Value);

            Assert.Equal("Test Description", post.Description);

        }

        
        [Fact]
        public void Can_create_new_Interest()
        {
            // Given
            var fakeId = new Guid("2443267e-87a1-4a55-bb36-e7e2499e58c1");
            
            var newPost = new PostForCreateDto() {
                Title = "My new brand test title",
                UserId = testUserId,
                Text = "This is my text to be encrypted",
                PostTagId = 

            };

            // Act
            var result = postController.Create(newPost).Result as OkObjectResult;

            // Assert
            Assert.IsType<PostPresenter>(result.Value);
                      
        }

        // [Fact]
        // public async Task Can_delete_an_Post()
        // {
        //     // Act
        //     var result = await postController.DeleteLan(testPostId) as NoContentResult;
        //     // Assert
        //     Assert.IsType<NoContentResult>(result);        
            
        // }

        // [Fact]
        // public async Task Can_update_an_Post() {
        //     // Given
        //     var langaugeToUpdate = mockRepo.Object.GetById(testPostId);
        
        
        //     // Act
        //     var result = await postController.UpdateLan(langaugeToUpdate.Result.Id, update) as NoContentResult;
        //     // Assert
        //     Assert.IsType<NoContentResult>(result);
        // }

    }
}