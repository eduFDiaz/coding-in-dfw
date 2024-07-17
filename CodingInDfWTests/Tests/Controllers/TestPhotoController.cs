using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
using Microsoft.AspNetCore.Http;
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

        Mock<IFormFile> mockFile;

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
            
            mockFile = new Mock<IFormFile>();

            mockPostPhotoRepo = new Mock<IRepository<PostPhoto>>();


            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid();
            

            listPhotos = new List<Photo>() {
                new Photo() { Description = "Test Photo", PublicId = "publicID" , IsMain = true, Url = "http://cloudinaryfake.com/image/4frds20", UserId = testUserId},
                new Photo() { Id = new Guid("26685234-a745-4040-b29e-c449a9f84d8c"), Description = "Test Photo 1", PublicId = "publicID 1" , IsMain = false, Url = "http://cloudinaryfake.com/image/4frds20",  UserId = testUserId},
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

            IOptions<CloudinarySettings> cloudConfig = Mock.Of<IOptions<CloudinarySettings>>(cn => cn.Value.ApiKey == "331921191438182"
            && cn.Value.CloudName == "www-codingindfw-com" && cn.Value.ApiSecret == "1d4WQFjCX8Y-nsurZ0iusG04tVA");

            PhotoController = new PhotoController(mockPostRepo.Object, mockPostPhotoRepo.Object, mockProductPhoto.Object
            ,mockPhotoRepo.Object, mockUserRepo.Object, mockProductRepo.Object, mockConfiguration.Object
            ,_mapper,cloudConfig);

            
        }

        // TODO: Make this works

        [Fact]
        public void Can_put_a_photo_as_a_main_productPhoto()
        {
            // Assemble
            var itemId = new Guid("ed8eb9cd-494b-4bd2-b9ec-0ed13a849edd");
            var photoId = new Guid("26685234-a745-4040-b29e-c449a9f84d8c");

            mockProductPhoto.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new ProductPhoto() {
                Id = new Guid("75c8c0ee-72e7-4e91-9a16-b549a5fef74c"),
                IsMain = false,
            });
            // Mocking a current main Photo
            mockProductPhoto.Setup(p => p.ListAll()).Returns(new List<ProductPhoto>() {
                new ProductPhoto() {
                    Id = new Guid("ab082be4-8acc-49af-8f21-3a7db97b1c49"),
                    IsMain = true,
                    ProductId = itemId
                }
            });

            // Save goes on
            mockProductPhoto.Setup(pr => pr.SaveAll()).ReturnsAsync(true);

            // Act
            var result = PhotoController.SetMain(itemId, "product", photoId).Result as NoContentResult;

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Cant_set_as_main_ProductPhoto_when_its_is_already_a_main_photo()
        {
            // Assemble
            var photoId = new Guid("26685234-a745-4040-b29e-c449a9f84d8c");
            mockProductPhoto.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new ProductPhoto() {
                Id = new Guid("75c8c0ee-72e7-4e91-9a16-b549a5fef74c"),
                IsMain = true,
            });

            // Act
            var result =  PhotoController.SetMain(new Guid(), "product", photoId).Result as BadRequestObjectResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void Cant_set_as_main_ProductPhoto_when_db_operation_fails()
        {
              // Assemble
            var itemId = new Guid("ed8eb9cd-494b-4bd2-b9ec-0ed13a849edd");
            var photoId = new Guid("26685234-a745-4040-b29e-c449a9f84d8c");

            mockProductPhoto.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new ProductPhoto() {
                Id = new Guid("75c8c0ee-72e7-4e91-9a16-b549a5fef74c"),
                IsMain = false,
            });
            // Mocking a current main Photo
            mockProductPhoto.Setup(p => p.ListAll()).Returns(new List<ProductPhoto>() {
                new ProductPhoto() {
                    Id = new Guid("ab082be4-8acc-49af-8f21-3a7db97b1c49"),
                    IsMain = true,
                    ProductId = itemId
                }
            });

            // Save goes on
            mockProductPhoto.Setup(pr => pr.SaveAll()).ReturnsAsync(false);

            // Act
            var result = PhotoController.SetMain(itemId, "product", photoId).Result as BadRequestObjectResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Can_put_a_photo_as_a_main_PostPhoto()
        {
            // Assemble
            var itemId = new Guid("ed8eb9cd-494b-4bd2-b9ec-0ed13a849edd");
            var photoId = new Guid("26685234-a745-4040-b29e-c449a9f84d8c");

            mockPostPhotoRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new PostPhoto() {
                Id = new Guid("75c8c0ee-72e7-4e91-9a16-b549a5fef74c"),
                IsMain = false,
            });
            // Mocking a current main Photo
            mockPostPhotoRepo.Setup(p => p.ListAll()).Returns(new List<PostPhoto>() {
                new PostPhoto{
                    Id = new Guid("ab082be4-8acc-49af-8f21-3a7db97b1c49"),
                    IsMain = true,
                    PostId = itemId
                }
            });

            // Save goes on
            mockPostPhotoRepo.Setup(pr => pr.SaveAll()).ReturnsAsync(true);

            // Act
            var result = PhotoController.SetMain(itemId, "post", photoId).Result as NoContentResult;

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Cant_set_as_main_PostPhoto_when_its_is_already_a_main_photo()
        {
            // Assemble
            var photoId = new Guid("26685234-a745-4040-b29e-c449a9f84d8c");
            mockPostPhotoRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new PostPhoto() {
                Id = new Guid("75c8c0ee-72e7-4e91-9a16-b549a5fef74c"),
                IsMain = true,
            });

            // Act
            var result =  PhotoController.SetMain(new Guid(), "post", photoId).Result as BadRequestObjectResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void Cant_set_as_main_PostPhoto_when_db_operation_fails()
        {
              // Assemble
            var itemId = new Guid("ed8eb9cd-494b-4bd2-b9ec-0ed13a849edd");
            var photoId = new Guid("26685234-a745-4040-b29e-c449a9f84d8c");

            mockPostPhotoRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new PostPhoto() {
                Id = new Guid("75c8c0ee-72e7-4e91-9a16-b549a5fef74c"),
                IsMain = false,
            });
            // Mocking a current main Photo
            mockPostPhotoRepo.Setup(p => p.ListAll()).Returns(new List<PostPhoto>() {
                new PostPhoto() {
                    Id = new Guid("ab082be4-8acc-49af-8f21-3a7db97b1c49"),
                    IsMain = true,
                    PostId = itemId
                }
            });

            // Save goes on
            mockPostPhotoRepo.Setup(pr => pr.SaveAll()).ReturnsAsync(false);

            // Act
            var result = PhotoController.SetMain(itemId, "post", photoId).Result as BadRequestObjectResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

         [Fact]
        public void Can_put_a_photo_as_a_main_UserPhoto()
        {
            // Assemble
            var UserId = new Guid("ed8eb9cd-494b-4bd2-b9ec-0ed13a849edd");
            var photoId = new Guid("26685234-a745-4040-b29e-c449a9f84d8c");

            mockPhotoRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Photo() {
                Id = new Guid("75c8c0ee-72e7-4e91-9a16-b549a5fef74c"),
                IsMain = false,
            });
            // Mocking a current main Photo
            mockPhotoRepo.Setup(p => p.ListAll()).Returns(new List<Photo>() {
                new Photo{
                    Id = new Guid("ab082be4-8acc-49af-8f21-3a7db97b1c49"),
                    IsMain = true,
                    UserId = UserId
                }
            });

            // Save goes on
            mockPhotoRepo.Setup(pr => pr.SaveAll()).ReturnsAsync(true);

            // Act
            var result = PhotoController.SetMain(UserId, "user", photoId).Result as NoContentResult;

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Cant_set_as_main_UserPhoto_when_its_is_already_a_main_photo()
        {
            // Assemble
            var photoId = new Guid("26685234-a745-4040-b29e-c449a9f84d8c");
            mockPhotoRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Photo() {
                Id = new Guid("75c8c0ee-72e7-4e91-9a16-b549a5fef74c"),
                IsMain = true,
            });

            // Act
            var result =  PhotoController.SetMain(new Guid(), "user", photoId).Result as BadRequestObjectResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void Cant_set_as_main_UserPhoto_when_db_operation_fails()
        {
              // Assemble
            var itemId = new Guid("ed8eb9cd-494b-4bd2-b9ec-0ed13a849edd");
            var photoId = new Guid("26685234-a745-4040-b29e-c449a9f84d8c");

            mockPhotoRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Photo() {
                Id = new Guid("75c8c0ee-72e7-4e91-9a16-b549a5fef74c"),
                IsMain = false,
            });
            // Mocking a current main Photo
            mockPhotoRepo.Setup(p => p.ListAll()).Returns(new List<Photo>() {
                new Photo() {
                    Id = new Guid("ab082be4-8acc-49af-8f21-3a7db97b1c49"),
                    IsMain = true,
                    UserId = itemId
                }
            });

            // Save goes on
            mockPostPhotoRepo.Setup(pr => pr.SaveAll()).ReturnsAsync(false);

            // Act
            var result = PhotoController.SetMain(itemId, "user", photoId).Result as BadRequestObjectResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void PhotoController_Returns_AllPhotos()
        {
          
            // Act
            var okResult = PhotoController.GetAllPhotos().Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
           
        }

        
        [Fact]
        public void Can_create_new_Photo()
        {
            // Assemble
            var userId = new Guid("7f1352df-4c79-46fe-989c-bcdcf3b8714e");

            var mockedFile = Mock.Of<IFormFile>(file => file.Length == 32 &&
            file.FileName == "testFile" &&
            file.Name == "TestName" );

            var fileMock = new Mock<IFormFile>();
            var physicalFile = new FileInfo("/home/karenydcruz/Desktop/43586145.png");
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            using (FileStream fs = physicalFile.OpenRead())
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                while (fs.Read(b, 0, b.Length) > 0)
                {
                    writer.WriteLine(temp.GetString(b));
                }
            }
            writer.Flush();
            ms.Position = 0;
            var fileName = physicalFile.Name;
            
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Name).Returns("Test Name");
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(m => m.OpenReadStream()).Returns(ms);
            fileMock.Setup(m => m.ContentDisposition).Returns(string.Format("inline; filename={0}", fileName));
            
            var file = fileMock.Object;

            var newPhoto = new PhotoForCreationDto() {
                File = file,
                Url = ""
                         
            };
            
            mockUserRepo.Setup(repo => repo.ListAsync()).ReturnsAsync( new List<User>() {
                new User() {
                    Id = userId
                }
            });
            // Act
            var result = PhotoController.AddPhotoForUser(userId, newPhoto).Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result.Value);
                      
        }

        [Fact]
        public async Task Cant_delete_a_Main_Photo()
        {
            // Assemble
            var testUserId = new Guid("b0388ada-1788-43e3-91ce-1793093c6b04");
            var testPhotoId = new Guid("3aaae9c8-26cd-4f84-b370-ab8fdceb9332");

            mockPhotoRepo.Setup(repo => repo.ListAsync()).ReturnsAsync( new List<Photo>() {
                new Photo() {
                    Id = testPhotoId,
                    UserId = testUserId
                }
            });
            
            // Act
            var result = await PhotoController.DeletePhoto(testUserId, testPhotoId) as BadRequestObjectResult;
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);        
            
        }

         [Fact]
        public async Task Can_delete_a_Photo()
        {
            // Assemble
            var testUserId = new Guid("b0388ada-1788-43e3-91ce-1793093c6b04");
            var testPhotoId = new Guid("3aaae9c8-26cd-4f84-b370-ab8fdceb9332");

            mockPhotoRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync( new Photo() {
                IsMain = false
            });

            mockPhotoRepo.Setup(repo => repo.Delete(It.IsAny<Photo>())).ReturnsAsync(true);

            mockPhotoRepo.Setup(repo => repo.ListAsync()).ReturnsAsync( new List<Photo>() {
                new Photo() {
                    Id = testPhotoId,
                    UserId = testUserId
                }
            });
            
            // Act
            var result = await PhotoController.DeletePhoto(testUserId, testPhotoId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

     

     }

    
}