using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models.Products;
using coding.API.Models.Products.ProductsRequirements;

namespace coding.API.Dtos.Products
{
    public class ProductForUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Url { get; set; }
        public string ProductPhoto { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        public ICollection<Guid> RequirementId { get; set; }
 
    }
}