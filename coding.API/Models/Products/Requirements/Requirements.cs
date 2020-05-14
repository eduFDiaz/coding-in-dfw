using System;
using System.Collections.Generic;
using coding.API.Models.Products.ProductsRequirements;

namespace coding.API.Models.Products.Requirements
{
    public class Requirement : BaseEntity
    {
        public string Description { get; set; }
        public ICollection<ProductRequirement> ProductRequirements { get; set; }

        
        
        
    }
}