using System;
using System.Collections.Generic;
using coding.API.Models.Entities.Products.ProductsRequirements;
using coding.API.Models.Entities.Products.Requirements;

namespace coding.API.Models.Entities.Products
{
    public class Product
    {
        public int Id { get; set; }
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
        public int UserId { get; set; }
        public ICollection<ProductRequirement> ProductRequirements { get; set; }
       

        // public List<Requirement> Requirements { get; set; }
        
        
    }
}