using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Helpers;
using coding.API.Models.Photos;
using coding.API.Models.Posts;
using coding.API.Models.Presenter;
using coding.API.Models.Products;
using coding.API.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace coding.API.Tests
{
    public class TestPhotoController
    {
        private readonly IMapper _mapper;
        Mock<IRepository<Photo>> mockPhotoRepo;
        Mock<IRepository<Post>> mockPostRepo;
        Mock<IRepository<PostPhoto>> MockPostPhotoRepo;
        Mock<IRepository<User>> MockUserRepo;
        Mock<IRepository<Product>> MockProductRepo;

        Mock<IRepository<ProductPhoto>> MockProductPhoto;

        Mock<IOptions<CloudinarySettings>> MockcloudinarySettings;

        Mock<IMapper> mockMapper;

        PhotoController PhotoController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        List<Photo> listPhotos;

        
       
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<PhotoForCreationDto, Photo>();
                mc.CreateMap<PhotoForReturnDto, Photo>();
               
            });
        }

        // Init test
        public TestPhotoController()
        {
         
            _mapper = new Mapper(CreateMaps());

            mockPhotoRepo = new Mock<IRepository<Photo>>();
            mockPostRepo = new Mock<IRepository<Post>>();
            MockProductRepo = new Mock<IRepository<Product>>();
            MockProductPhoto = new Mock<IRepository<ProductPhoto>>();
            MockUserRepo = new Mock<IRepository<User>>();
            MockcloudinarySettings = new Mock<IOptions<CloudinarySettings>>();
            MockPostPhotoRepo = new Mock<IRepository<PostPhoto>>();

            mockMapper = new Mock<IMapper>();

            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid();
            

            listPhotos = new List<Photo>() {
                new Photo() { Description = "Test Photo", PublicId = "publicID" , IsMain = true, Url = "http://cloudinaryfake.com/image/4frds20", UserId = testUserId},
                new Photo() { Description = "Test Photo 1", PublicId = "publicID 1" , IsMain = false, Url = "http://cloudinaryfake.com/image/4frds20",  UserId = testUserId},
            };

          
            
        }

        // TODO: Make this works

        // [Fact]
        // public void PhotoController_Returns_AllPhotos()
        // {
          
        //     mockPhotoRepo.Setup(photoRepo => photoRepo.ListAsync()).ReturnsAsync(listPhotos);
        //     mockPostRepo.Setup(postRepo => postRepo.Add(It.IsAny<Post>())).ReturnsAsync(new Post());
            

        //     PhotoController = new PhotoController(mockPostRepo.Object, MockPostPhotoRepo.Object,
        //     MockProductPhoto.Object, mockPhotoRepo.Object, MockUserRepo.Object, MockProductRepo.Object,
        //     mockConfiguration.Object, mockMapper.Object, MockcloudinarySettings.Object);


        //     // Act
        //     var okResult = PhotoController.GetAllPhotos().Result as OkObjectResult;

        //     // Assert
        //     Assert.IsType<OkObjectResult>(okResult);

        //     // var items = Assert.IsType<List<Photo>>(okResult.Value);

        //     // Assert.Equal(2020, items[0].Year);
        //     // Assert.Equal(testUserId, items[1].UserId);
            
        // }

        // [Fact]
        // public void Check_PhotoList_Returned()
        // {
        //     // Act
        //     var okResult = PhotoController.GetPhotoForUser(testUserId).Result as OkObjectResult;
        //     // Assert
        //     var items = Assert.IsType<List<Photo>>(okResult.Value);
        //     Assert.Equal(2, items.Count);
        // }

        // [Fact]
        // public void Can_create_new_Photo()
        // {
        //     // Given
        //     var newPhoto = new CreatePhotoDto() {
        //         Company = "New Company",
        //         Title = "New Photo",
        //         UserId = testUserId,
        //         Year = 2020
        //     };

        //     // Act
        //     var result = PhotoController.Create(newPhoto).Result as OkObjectResult;

        //     // Assert
        //     Assert.IsType<PhotoPresenter>(result.Value);
                      
        // }

        // [Fact]
        // public async Task Can_delete_an_Photo()
        // {
        //     // Act
        //     var result = await PhotoController.DeletePhoto(testPhotoId) as NoContentResult;
        //     // Assert
        //     Assert.IsType<NoContentResult>(result);        
            
        // }

        // [Fact]
        // public async Task Can_update_an_Photo() {
        //     // Given
        //     var PhotoToUpdate = mockRepo.Object.GetById(testPhotoId);
        
        
        //     // Act
        //     var result = await PhotoController.UpdatePhoto(PhotoToUpdate.Result.Id, update) as NoContentResult;
        //     // Assert
        //     Assert.IsType<NoContentResult>(result);
        // }

     }

    
}