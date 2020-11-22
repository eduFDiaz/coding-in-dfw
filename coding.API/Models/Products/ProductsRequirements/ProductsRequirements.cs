using System;
using System.Diagnostics.CodeAnalysis;
using coding.API.Models.Products.Requirements;

namespace coding.API.Models.Products.ProductsRequirements
{
    [ExcludeFromCodeCoverage]
    public class ProductRequirement : BaseEntity
    {
        
     
        public Guid ProductId { get; set; }
        public Guid RequirementId { get; set; }
        public Product Product { get; set; }
        
        public Requirement Requirement { get; set; }
    }
}
