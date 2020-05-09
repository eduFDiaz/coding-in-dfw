using System.Collections.Generic;
using coding.API.Models.Entities.Products;
using coding.API.Models.Entities.Products.Requirements;


namespace coding.API.Models.Entities.Products.ProductsRequirements
{
    public class ProductRequirement
    {
        
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int RequirementId { get; set; }
        public Requirement Requirement { get; set; }
    }
}
