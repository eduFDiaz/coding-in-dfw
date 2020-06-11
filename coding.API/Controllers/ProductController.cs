using AutoMapper;
using coding.API.Data;
using coding.API.Dtos.Products;
using coding.API.Dtos;
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
using coding.API.Models.Photos;

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly Repository<Product> _productDal;

        private readonly Repository<ProductPhoto> _productPhotoDal;

        private readonly Repository<Requirement> _requirementDal;

        private readonly Repository<ProductRequirement> _productRequirementDal;


        public ProductController(
            Repository<Product> productDal,
            Repository<ProductPhoto> productPhotoDal,
            IConfiguration config, IMapper mapper,
            Repository<Requirement> requirementDal,
            Repository<ProductRequirement> productRequirementDal
            )
        {

            _productDal = productDal;
            _productPhotoDal = productPhotoDal;
            _config = config;
            _mapper = mapper;
            _requirementDal = requirementDal;
            _productRequirementDal = productRequirementDal;
        }
        [Authorize]
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

            return Ok(new NewProductPresenter(createdProduct));

        }


        [HttpGet("foruser/{userid}")]
        public async Task<IActionResult> GetAllProductsForuser(Guid userid)
        {
            var allUserProducts = (await _productDal.GetRelatedFields("ProductRequirements.Requirement", "Photos")).Where(p => p.UserId == userid).ToList(); ;

            List<ProductPresenter> allprods = new List<ProductPresenter>();

            foreach (var product in allUserProducts)
            {
                allprods.Add(new ProductPresenter(product));
            }

            var producCount = allUserProducts.Count;

            if (producCount == 0)
                return NotFound("There is no products here");

            return Ok(allprods);
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult> GetProducts()
        {
            var products = (await _productDal.GetRelatedFields("ProductRequirements.Requirement", "Photos")).ToList();

            var productsToReturn = _mapper.Map<List<ProductForDetailDto>>(products);

            return Ok(productsToReturn);
        }
        
        
        [HttpGet("{productid}")]
        public async Task<IActionResult> GetSingleProduct(Guid productid)
        {
            var product = (await _productDal.GetRelatedFields("ProductRequirements.Requirement", "Photos")).Where(p => p.Id == productid).SingleOrDefault();

            var output = _mapper.Map<ProductForDetailDto>(product);

            return Ok(new ProductPresenter(product));
        }

        [Authorize]
        [HttpDelete("{productid}/delete")]
        public async Task<IActionResult> DeleteTag(Guid productid)
        {
            var productToDelete = (await _productDal.GetById(productid));

            if (await _productDal.Delete(productToDelete))
                return NoContent();

            return BadRequest("cant delete the Product");

        }

        [Authorize]
        [HttpPut("{productId}/update")]
        public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] ProductForUpdateDto request)
        {
            var productToEdit = (await _productDal.GetRelatedField("ProductRequirements.Requirement")).FirstOrDefault(p => p.Id == productId);

            var pr = new ProductRequirementForCreateDto();


            foreach (var row in productToEdit.ProductRequirements)
            {

                var record = _productRequirementDal.GetRelatedRowPR(productId, row.RequirementId);

                if (record != null)
                {
                    _productRequirementDal.DeleteSync(record);
                }

            }

            foreach (var Requirement in request.Requirements)
            {

                pr.RequirementId = Requirement;

                pr.ProductId = productToEdit.Id;

                pr.Requirement = await _requirementDal.GetById(Requirement);

                var productRequirementToUpdate = _mapper.Map<ProductRequirement>(pr);

                await _productRequirementDal.Add(productRequirementToUpdate);

            }


            productToEdit.Industry = request.Industry;
            productToEdit.BodyText = request.BodyText;
            productToEdit.ClientName = request.ClientName;
            productToEdit.Name = request.Name;
            productToEdit.ProductDescription = request.Description;
            productToEdit.ProjectIntro = request.ProjectIntro;
            productToEdit.ShortResume = request.ShortResume;
            productToEdit.Size = request.Size;
            productToEdit.Type = request.Type;
            productToEdit.Url = request.Url;


            if (await _productDal.Update(productToEdit))
                return Ok(new NewProductPresenter(productToEdit));

            return BadRequest("Cant update the product");

        }
        [Authorize]
        [HttpPost("addRequirement")]
        public async Task<IActionResult> NewRequeriment([FromBody] RequirementForCreationDto request)
        {
            var requirementToCreate = new Requirement()
            {
                Description = request.Description
            };

            var createdRequirement = await _requirementDal.Add(requirementToCreate);

            return Ok(new RequirementPresenter(createdRequirement));

        }

        [Authorize]
        [HttpGet("requirements/all")]
        public async Task<ActionResult> GetRequirements()
        {
            var requirements = (await _requirementDal.ListAsync());

            var requirementsToReturn = _mapper.Map<List<RequirementForDetailDto>>(requirements);

            return Ok(requirementsToReturn);
        }
        [Authorize]
        [HttpDelete("requirement/{requirementId}/delete")]
        public async Task<IActionResult> DeleteRequirement(Guid requirementId)
        {
            var reqtoDel = (await _requirementDal.GetById(requirementId));

            if (await _requirementDal.Delete(reqtoDel))
                return NoContent();

            return BadRequest("Cant Delete the Requirement");
        }

    }
}