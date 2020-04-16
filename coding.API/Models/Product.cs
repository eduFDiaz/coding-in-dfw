using System;
using System.Collections.Generic;

namespace coding.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string ProductPhoto { get; set; }
        public string ProductDescription { get; set; }
        
    }
}