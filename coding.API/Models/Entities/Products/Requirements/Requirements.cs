using System;
using System.Collections.Generic;

using coding.API.Models.Entities.Products.ProductsRequirements;

namespace coding.API.Models.Entities.Products.Requirements
{
    public class Requirement
    {
        
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<ProductRequirement> ProductRequirements { get; set; }

        // public Product Product { get; set; }
        
        
    }
}