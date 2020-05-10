using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using coding.API.Dtos.Requirements;


namespace coding.API.Dtos.Products
{
    public class ProductForDetailDto
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string ProductPhoto { get; set; }
        public string ProductDescription { get; set; }
        public int  UserId { get; set; }
        public string ProjectIntro { get; set; }
        public string ShortResume { get; set; }
        public string ClientName { get; set; }
        public string BodyText { get; set; }
        public string Industry { get; set; }
        public int Size { get; set; }
        // public ICollection<int> RequirementId { get; set; }
        // public ICollection<RequirementForDetailDto> Requirements { get; set; }

        public List<ProductRequirementForDetailDto> ProductRequirements {get; set;}
    }
}