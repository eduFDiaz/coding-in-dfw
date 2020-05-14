using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace coding.API.Dtos.Products
{
    public class ProductForCreateDto
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
        [Required]
        public Guid  UserId { get; set; }
        public string ProjectIntro { get; set; }
        public string ShortResume { get; set; }
        public string ClientName { get; set; }
        public string BodyText { get; set; }
        public string Industry { get; set; }
        public int Size { get; set; }
        
        public ICollection<Guid> RequirementId { get; set; }
    }
}