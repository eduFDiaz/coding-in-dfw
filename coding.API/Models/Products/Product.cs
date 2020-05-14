using System;
using System.Collections.Generic;
using coding.API.Models.Products.ProductsRequirements;
using coding.API.Models.Products.Requirements;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace coding.API.Models.Products
{
    [Table("Product")]
    public partial class Product : BaseEntity
    {
        [Key]
        public string Name { get; set; }
        public string ProjectIntro { get; set; }
        public string ShortResume { get; set; }
        public string ClientName { get; set; }
        public string BodyText { get; set; }
        public string Industry { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string ProductPhoto { get; set; }
        public string ProductDescription { get; set; }
        public int Size { get; set; }
        public Guid UserId { get; set; }
        public List<ProductRequirement> ProductRequirements { get; set; }

        public Requirement Requirement { get; set; }
        
    }
}