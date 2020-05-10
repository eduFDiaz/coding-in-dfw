using coding.API.Dtos.Requirements;
using coding.API.Models.Entities.Products;
using coding.API.Models.Entities.Products.Requirements;

namespace coding.API.Dtos
{
    public class ProductRequirementForDetailDto
    {
        public int ProductId { get; set; }
        public int RequirementId { get; set; }

        public RequirementForDetailDto Requirement { get; set; }

    }
}