using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models.Entities.Products;

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
        public int  UserId { get; set; }
    }
}