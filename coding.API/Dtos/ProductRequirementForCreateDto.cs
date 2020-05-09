using coding.API.Models.Entities.Products;
using coding.API.Models.Entities.Products.Requirements;

namespace coding.API.Dtos
{
    public class ProductRequirementForCreateDto
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public int RequirementId { get; set; }

        public Requirement Requirement { get; set; }
    }
}