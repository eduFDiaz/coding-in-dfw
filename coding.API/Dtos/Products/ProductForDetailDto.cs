using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Dtos.Requirements;
using coding.API.Models.Products;
using coding.API.Models.Products.Requirements;

namespace coding.API.Dtos.Products
{
    public class ProductForDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public List<ProductPhotoForDetailDto> Photos { get; set; }

        public string ProductDescription { get; set; }

        public string ProjectIntro { get; set; }
        public string ShortResume { get; set; }
        public string ClientName { get; set; }
        public string BodyText { get; set; }
        public string Industry { get; set; }
        public int Size { get; set; }

        // public ICollection<Guid> RequirementId { get; set; }

        public List<ProductRequirementForDetailDto> ProductRequirements { get; set; }
        //public List<RequirementForDetailDto> Requirements { get; set; }
        // public List<Requirement> Requirements { get; set; }

        //public List<ProductRequirementForDetailDto> ProductRequirements { get; set; } 
        // public List<ProductRequirementForDetailDto> ProductRequirements {get; set;} this works


    }
}