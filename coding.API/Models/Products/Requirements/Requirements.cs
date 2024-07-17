using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using coding.API.Models.Products.ProductsRequirements;

namespace coding.API.Models.Products.Requirements
{
        [ExcludeFromCodeCoverage]
    public class Requirement : BaseEntity
    {
        public string Description { get; set; }
        public List<ProductRequirement> ProductRequirements { get; set; }

        
        
        
    }
}