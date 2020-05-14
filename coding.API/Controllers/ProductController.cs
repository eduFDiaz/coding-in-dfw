using AutoMapper;
using coding.API.Data;
using coding.API.Dtos.Products;
using coding.API.Dtos;
using coding.API.Models;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using coding.API.Models.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using coding.API.Dtos.Requirements;
using coding.API.Models.Products.Requirements;
using coding.API.Models.Products.ProductsRequirements;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly Repository<Product> _productDal;

        private readonly Repository<Requirement> _requirementDal;

        private readonly Repository<ProductRequirement> _productRequirementDal;
        

        public ProductController(
            Repository<Product> productDal, 
            IConfiguration config, IMapper mapper,
            Repository<Requirement> requirementDal,
            Repository<ProductRequirement> productRequirementDal )
        {
            
            _productDal = productDal;
            _config = config;
            _mapper = mapper;
            _requirementDal = requirementDal;
            _productRequirementDal = productRequirementDal;
        }

        
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ProductForCreateDto request)
        {

            var product = _mapper.Map<Product>(request);
            
            var pr = new ProductRequirementForCreateDto();              

            var createdProduct = await _productDal.Add(product);

             foreach (var Requirement in request.RequirementId)
            {
               
                pr.RequirementId = Requirement;
               
                pr.ProductId = createdProduct.Id;

                pr.Requirement = await _requirementDal.GetById(Requirement);

                var productRequirementToCreate = _mapper.Map<ProductRequirement>(pr);
               
                await _productRequirementDal.Add(productRequirementToCreate);
                
            }

           return Ok(new ProductPresenter(createdProduct));
           
        } 

        
        [HttpGet("{userid}")]
        public async Task<IActionResult> GetAllProductsForuser(Guid userid)
        {
            var allUserProducts = (await _productDal.ListAsync()).Where(p => p.UserId == userid).ToList();

            var producCount = allUserProducts.Count;

            if (producCount == 0)
                return NotFound("There is no products here");

            var requirement = (await _productDal.GetProductRequirementIncluded()).Select(pr => pr.Requirement.Description).ToList();
            // var requirement = (await _productDal.GetProductRequirementIncluded()).ToList();

            // var test2 = (await _productRequirementDal.ListAsync()).Select(pr => pr.Requirement).ToList();

             var outPut = _mapper.Map<List<ProductForDetailDto>>(allUserProducts);
       
            return Ok(outPut);
        }

        [HttpGet("product/{productid}")]
        public async Task<IActionResult> GetSingleProduct(Guid productid)
        {
            var product = (await _productDal.GetById(productid));

            return Ok( new ProductPresenter(product));
        }

        
        [HttpDelete("{productid}/delete")]
        public async Task<IActionResult> DeleteTag(Guid productid)
        {
            var productToDelete = (await _productDal.GetById(productid));

            await _productDal.Delete(productToDelete);

            if (await _productDal.SaveAll())
                return NoContent();

            return BadRequest("cant delete the Product");

        }

        
        [HttpPut("{productid}/update")]
        public async Task<IActionResult> UpdateTag(Guid productId, [FromBody] ProductForUpdateDto request)
        {
            var productToEdit = (await _productDal.GetById(productId));

        //     productToEdit.Name = request.Name;
        //  productToEdit.Type = request.Type;
        // productToEdit.Url = request.Url;
        // productToEdit.ProductDescription = request.ProductDescription;
            
           var toUpd = _mapper.Map(request, productToEdit);
            
            await _productDal.Update(toUpd);

            if (await _productDal.SaveAll())
                return NoContent();
            
            return BadRequest("Catn uptade product");

        }

         [HttpPost("addRequirement")]
        public async Task<IActionResult> NewRequeriment([FromBody] RequirementForCreationDto request)
        {
            var requirementToCreate = new Requirement()
            {
                Description = request.Description
            };

            var createdRequirement = await _requirementDal.Add(requirementToCreate);

            // var requirementToShow = _mapper.Map<RequirementForDetailDto>(createdRequirement);

            return Ok(new RequirementPresenter(createdRequirement));
                        
        }
        

    }
}