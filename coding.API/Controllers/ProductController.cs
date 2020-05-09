using coding.API.Models.Entities.Products;
using coding.API.Models.Entities.Products.Requirements;
using System.Collections.Generic;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using coding.API.Dtos.Requirements;


using coding.API.Models.Interfaces;
using coding.API.Models.Entities.Products.ProductsRequirements;
using coding.API.Dtos;
using coding.API.Models.Presenter;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductRepo _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public ProductController(IProductRepo repo, IConfiguration config, IMapper mapper)
        {
            this._repo = repo;
            this._config = config;
            this._mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ProductForCreateDto productForCreateDto)
        {

            // Creo una nueva instancia de PostTag asociandola con un Dto.
            var pr = new ProductRequirementForCreateDto();

            //Lo mismo pero con el post que voy a crear
            var productToCreate = _mapper.Map<Product>(productForCreateDto);

            //Creo el post (sin los tags ni nada)
            var createdProduct = await _repo.Create(productToCreate);

            // Itero por los ids que recibo del usuario
            foreach (var Requirement in productForCreateDto.RequirementId)
            {
                // Asigno el TagId
                pr.RequirementId = Requirement;
                // Asigno el PostId
                pr.ProductId = createdProduct.Id;

                pr.Requirement = await _repo.GetRequirementById(Requirement);

                pr.Product = await _repo.GetProduct(createdProduct.Id);
                // Mapeo a postTag
                var productRequirementToCreate = _mapper.Map<ProductRequirement>(pr);
                // Guardo
                await _repo.addRequirementToProduct(productRequirementToCreate);
                             
                
            }
            //Guardo todo
            await _repo.SaveAll();

            // var postToReturn = _mapper.Map<PostForDetailDto>(createdPost);
            //Retorno el post que recien se creo.                   
            return Ok(new ProductPresenter(createdProduct));
            
        }

        [HttpGet("{userid}", Name = "GetProduct")]
        public async Task<IActionResult> GetAllUserProducts(int userid)
        {
            var productsFromRepo = await _repo.GetAllUserProducts(userid);

            var productsToReturn = _mapper.Map<List<ProductForDetailDto>>(productsFromRepo);

            var Size = productsToReturn.Count;

            if (Size == 0)
                return NotFound();

            return Ok(productsToReturn);
        }

        // [Authorize]
        [HttpDelete("{productid}/delete", Name = "DeteleProduct")]
        public async Task<IActionResult> DeleteProduct(int productid)
        {
                           
            var productsFromRepo = await _repo.DeleteProduct(productid);

            if (!productsFromRepo)
                return NotFound();
    
            return NoContent();
                        
        }

        [HttpPut("{productid}/update", Name = "Update Product")]
        public async Task<IActionResult> UpdatePost(int productid, [FromBody] ProductForUpdateDto productForUpdateDto)
        {
            var productFromRepo = await _repo.GetProduct(productid);

            if (productFromRepo == null)
                return NotFound();
                
            _mapper.Map(productForUpdateDto, productFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Failed update Product");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var allproducts = await _repo.GetAll();

            return Ok(allproducts);
        }

        [HttpPost("addRequirement")]
        public async Task<IActionResult> NewRequeriment([FromBody] RequirementForCreationDto request)
        {
            var requirementToCreate = _mapper.Map<Requirement>(request);

            var createdRequirement = await _repo.CreateRequirement(requirementToCreate);

            // var requirementToShow = _mapper.Map<RequirementForDetailDto>(createdRequirement);

            if (await _repo.SaveAll())
                return Ok(new RequirementPresenter(createdRequirement));
            
            return BadRequest();
        }
    
    }
}