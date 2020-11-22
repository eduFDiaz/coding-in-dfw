using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Controllers;
using coding.API.Data;
using coding.API.Dtos;
using coding.API.Dtos.Products;
using coding.API.Models.Photos;
using coding.API.Models.Presenter;
using coding.API.Models.Products;
using coding.API.Models.Products.ProductsRequirements;
using coding.API.Models.Products.Requirements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;



namespace coding.API.Tests
{
    public class TestProductsController
    {
        private readonly IMapper _mapper;
        Mock<IRepository<Product>> mockRepo;
        private Mock<IRepository<ProductPhoto>> mockProductPhotoRepo;
        private Mock<IRepository<Requirement>> mockRequirementRepo;
        private Mock<IRepository<ProductRequirement>> mockProductRequirementRepo;
        
        ProductController ProductController;
        Mock<IConfiguration> mockConfiguration;

        Guid testUserId;

        Guid testProductId;

        List<Product> listProducts;

        
        ProductForUpdateDto update;
        // Define AutoMapper mappings :D
        private MapperConfiguration CreateMaps()
{
                return new MapperConfiguration(mc =>
            {
                mc.CreateMap<ProductForUpdateDto, Product>();
                mc.CreateMap<ProductForCreateDto, Product>();
                mc.CreateMap<ProductRequirementForCreateDto, ProductRequirement>();
                mc.CreateMap<List<Product>, ProductForDetailDto>();
                mc.CreateMap<ProductForDetailDto, Product>();
               
            });
        }
        // Init test
        public TestProductsController()
        {

            
            _mapper = new Mapper(CreateMaps());

            mockRepo = new Mock<IRepository<Product>>();

            mockProductPhotoRepo = new Mock<IRepository<ProductPhoto>>();

            mockRequirementRepo = new Mock<IRepository<Requirement>>();

            mockProductRequirementRepo = new Mock<IRepository<ProductRequirement>>();
            
            mockConfiguration = new Mock<IConfiguration>();

            testUserId = new Guid("968258bd-7198-464f-855e-18604fc1f870");
            testProductId = new Guid("23de061b-cb8e-46c1-b691-cd354fa1216b");

            listProducts = new List<Product>() {
                new Product() { 
                    Id = testProductId,
                    UserId = testUserId,
                    BodyText = "Body text for product",
                    ClientName = "Test Client Name",
                    Industry = "Test Industry",
                    Name = "Test Product Name",
                    ProductDescription = "Test Product Description",
                    ProductRequirements = new List<ProductRequirement>() {
                        new ProductRequirement() {
                            Id = new Guid("cd2d95c4-d207-4abf-8816-a588cb919574"),
                            ProductId = testProductId
                        }
                    },
                    ProjectIntro = "Test Project Intro",
                    ShortResume = "Test Short Resume",
                    Size = 100,
                    Type = "Web App Test Project",
                    Url = "http://www.mytesturl.com"
                }
            };

            
            mockRepo.Setup(repo => repo.Add(It.IsAny<Product>())).ReturnsAsync(listProducts[0]);
            mockRepo.Setup(repo => repo.ListAll()).Returns(listProducts).Verifiable();
            mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(listProducts);
            mockRepo.Setup(repo => repo.GetRelatedFields(It.IsAny<String>(), It.IsAny<String>())).ReturnsAsync(listProducts);
            mockRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(listProducts[0]);
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Product>())).ReturnsAsync(true);
            mockRepo.Setup(repo => repo.Update(It.IsAny<Product>())).ReturnsAsync(true);



            ProductController = new ProductController(mockRepo.Object,mockProductPhotoRepo.Object,mockConfiguration.Object, _mapper, mockRequirementRepo.Object,mockProductRequirementRepo.Object);
            
        }

        [Fact]
        public void ProductController_Returns_GetById()
        {
            // Act
            var okResult = ProductController.GetAllProductsForuser(testUserId).Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);

            var items = Assert.IsType<List<ProductPresenter>>(okResult.Value);

            Assert.Equal("Test Client Name", items[0].ClientName);
            Assert.Equal(testProductId, items[0].Id);
            
        }

        [Fact]
        public void Can_create_new_Product()
        {
            // Given
            mockRequirementRepo.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(new Requirement() {
                Description = "Test Requierement",
                Id = new Guid("2774342a-66f0-41e7-8f5e-97ddcf06c425")
            });

            mockProductRequirementRepo.Setup(repo => repo.Add(It.IsAny<ProductRequirement>())).ReturnsAsync(new ProductRequirement() {
                Id = new Guid("3ffe7da4-f0b4-4521-9db7-c365513166db"),
                ProductId = testProductId,
                RequirementId = new Guid("2774342a-66f0-41e7-8f5e-97ddcf06c425")
            });

            var newProduct = new ProductForCreateDto() {
                UserId = testUserId,
                BodyText = "Body text for product",
                ClientName = "Test Client Name",
                Industry = "Test Industry",
                Name = "Test Product Name",
                ProductDescription = "Test Product Description",
                ProjectIntro = "Test Project Intro",
                ShortResume = "Test Short Resume",
                Size = 100,
                Type = "Web App Test Project",
                Url = "http://www.mytesturl.com",
                RequirementId = new Collection<Guid>() {
                    new Guid("7749f6e9-0d5a-4c4c-abb0-882d5237ca4a"),
                    new Guid("cb4dac6d-ede0-4f5a-8118-ed556cd58ab1")
                }
                
            };

            // Act
            var result = ProductController.Create(newProduct).Result as OkObjectResult;

            // Assert
            Assert.IsType<NewProductPresenter>(result.Value);
                      
        }

        [Fact]
        public async Task Can_delete_an_Product()
        {
            // Act
            var result = await ProductController.DeleteProduct(testProductId) as NoContentResult;
            // Assert
            Assert.IsType<NoContentResult>(result);        
            
        }

        [Fact]
        public async Task Can_update_an_Product() {
            // Assemble
            mockRepo.Setup(repo => repo.GetRelatedField(It.IsAny<String>())).ReturnsAsync(listProducts);
            var ProductToUpdate = mockRepo.Object.GetById(testProductId);

            update = new ProductForUpdateDto() {
                BodyText = "tesasdasdasdasd",
                ClientName = "update Client Name",
                Description = "Description test",
                Industry = "Tech",
                Name = "Test name",
                Requirements = new Collection<Guid>() {
                    new Guid("a4b93693-a52f-46ef-b13d-0f3736b22a30"),
                    new Guid("dcf36e19-2041-427c-9a98-eacc8c8b03a2")
                }
            };
        
        
            // Act
            var result = await ProductController.UpdateProduct(ProductToUpdate.Result.Id, update) as OkObjectResult;
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async  Task Can_get_products()
        {
             // Act
            var result = await ProductController.GetProducts() as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async  Task Can_get_single_product()
        {
             // Act
            var result = await ProductController.GetSingleProduct(testProductId) as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }



     }

    
}