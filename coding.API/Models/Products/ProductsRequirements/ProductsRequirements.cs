using System;
using coding.API.Models.Products.Requirements;

namespace coding.API.Models.Products.ProductsRequirements
{
    public class ProductRequirement : BaseEntity
    {
        
     
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid RequirementId { get; set; }
        public Requirement Requirement { get; set; }
    }
}
