using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Helpers;
using coding.API.Models.Photos;
using coding.API.Models.Posts;
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
         private Account _account;
        Mock<IRepository<Photo>> mockPhotoRepo;
        Mock<IRepository<Post>> mockPostRepo;
        Mock<IRepository<PostPhoto>> mockPostPhotoRepo;
        Mock<IRepository<User>> mockUserRepo;
        Mock<IRepository<Product>> mockProductRepo;

        Mock<IRepository<ProductPhoto>> mockProductPhoto;

        Mock<IOptions<CloudinarySettings>> mockcloudinarySettings;

        
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
            mockProductRepo = new Mock<IRepository<Product>>();
            mockProductPhoto = new Mock<IRepository<ProductPhoto>>();
            mockUserRepo = new Mock<IRepository<User>>();
            mockcloudinarySettings = new Mock<IOptions<CloudinarySettings>>();
            mockPostPhotoRepo = new Mock<IRepository<PostPhoto>>();


            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid();
            

            listPhotos = new List<Photo>() {
                new Photo() { Description = "Test Photo", PublicId = "publicID" , IsMain = true, Url = "http://cloudinaryfake.com/image/4frds20", UserId = testUserId},
                new Photo() { Description = "Test Photo 1", PublicId = "publicID 1" , IsMain = false, Url = "http://cloudinaryfake.com/image/4frds20",  UserId = testUserId},
            };

            // MockPhoto Repo
            mockPhotoRepo.Setup(repo => repo.Add(It.IsAny<Photo>())).ReturnsAsync(listPhotos[0]);
            mockPhotoRepo.Setup(repo => repo.ListAll()).Returns(listPhotos).Verifiable();
            mockPhotoRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listPhotos);
            mockPhotoRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(listPhotos[0]);
            mockPhotoRepo.Setup(repo => repo.Delete(It.IsAny<Photo>())).ReturnsAsync(true);
            mockPhotoRepo.Setup(repo => repo.Update(It.IsAny<Photo>())).ReturnsAsync(true);

            // Mock Posts
            mockPostRepo.Setup(repo => repo.Add(It.IsAny<Post>())).ReturnsAsync(new Post());
            mockPostRepo.Setup(repo => repo.ListAll()).Returns(new List<Post>()).Verifiable();
            mockPostRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(new List<Post>());
            mockPostRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Post());
            mockPostRepo.Setup(repo => repo.Delete(It.IsAny<Post>())).ReturnsAsync(true);
            mockPostRepo.Setup(repo => repo.Update(It.IsAny<Post>())).ReturnsAsync(true);

            // Mock Post Photo
            mockPostPhotoRepo.Setup(repo => repo.Add(It.IsAny<PostPhoto>())).ReturnsAsync(new PostPhoto());
            mockPostPhotoRepo.Setup(repo => repo.ListAll()).Returns(new List<PostPhoto>()).Verifiable();
            mockPostPhotoRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(new List<PostPhoto>());
            mockPostPhotoRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new PostPhoto());
            mockPostPhotoRepo.Setup(repo => repo.Delete(It.IsAny<PostPhoto>())).ReturnsAsync(true);
            mockPostPhotoRepo.Setup(repo => repo.Update(It.IsAny<PostPhoto>())).ReturnsAsync(true);

            // Mock Product Photo
            mockProductPhoto.Setup(repo => repo.Add(It.IsAny<ProductPhoto>())).ReturnsAsync(new ProductPhoto());
            mockProductPhoto.Setup(repo => repo.ListAll()).Returns(new List<ProductPhoto>());
            mockProductPhoto.Setup(repo => repo.ListAsync()).ReturnsAsync(new List<ProductPhoto>());
            mockProductPhoto.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new ProductPhoto());
            mockProductPhoto.Setup(repo => repo.Delete(It.IsAny<ProductPhoto>())).ReturnsAsync(true);
            mockProductPhoto.Setup(repo => repo.Update(It.IsAny<ProductPhoto>())).ReturnsAsync(true);

            // Mock User Photo
            mockUserRepo.Setup(repo => repo.Add(It.IsAny<User>())).ReturnsAsync(new User());
            mockUserRepo.Setup(repo => repo.ListAll()).Returns(new List<User>());
            mockUserRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(new List<User>());
            mockUserRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new User());
            mockUserRepo.Setup(repo => repo.Delete(It.IsAny<User>())).ReturnsAsync(true);
            mockUserRepo.Setup(repo => repo.Update(It.IsAny<User>())).ReturnsAsync(true);

            // Mock Product Repo
            mockProductRepo.Setup(repo => repo.Add(It.IsAny<Product>())).ReturnsAsync(new Product());
            mockProductRepo.Setup(repo => repo.ListAll()).Returns(new List<Product>());
            mockProductRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(new List<Product>());
            mockProductRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Product());
            mockProductRepo.Setup(repo => repo.Delete(It.IsAny<Product>())).ReturnsAsync(true);
            mockProductRepo.Setup(repo => repo.Update(It.IsAny<Product>())).ReturnsAsync(true);

            

            PhotoController = new PhotoController(mockPostRepo.Object, mockPostPhotoRepo.Object, mockProductPhoto.Object
            ,mockPhotoRepo.Object, mockUserRepo.Object, mockProductRepo.Object, mockConfiguration.Object
            ,_mapper,mockcloudinarySettings.Object);

            
        }

        // TODO: Make this works

        [Fact]
        public void Can_put_a_photo_as_a_main()
        {
            var itemId = new Guid("ed8eb9cd-494b-4bd2-b9ec-0ed13a849edd");
            var photoId = new Guid("a3317872-6841-45c1-b134-7e0ac245c4be");

            // Act
            var result = PhotoController.SetMain(itemId, "Product", photoId).Result as NoContentResult;

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        // [Fact]
        // public void PhotoController_Returns_AllPhotos()
        // {
          
        //     // Act
        //     var okResult = PhotoController.GetAllPhotos().Result as OkObjectResult;

        //     // Assert
        //     Assert.IsType<OkObjectResult>(okResult);
           
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