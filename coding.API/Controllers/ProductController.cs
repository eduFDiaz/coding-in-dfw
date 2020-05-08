using coding.API.Models.Entities.Products;
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

using coding.API.Models.Interfaces;



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
        public async Task<Product> Create([FromBody] ProductForCreateDto productForCreateDto)
        {

            var productToCreate = _mapper.Map<Product>(productForCreateDto);

            var createdProduct = await _repo.Create(productToCreate);

            return createdProduct;
            
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
    }
}