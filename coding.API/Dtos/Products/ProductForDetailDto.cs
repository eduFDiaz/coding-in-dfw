using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models.Products;

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
    }
}