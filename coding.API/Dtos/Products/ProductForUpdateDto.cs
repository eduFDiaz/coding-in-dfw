using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        // public string ProductPhoto { get; set; }
        [Required]
        public string Description { get; set; }

        public int Size { get; set; }

        public string ProjectIntro { get; set; }
        public string ShortResume { get; set; }
        public string ClientName { get; set; }
        public string Text { get; set; }
        public string Industry { get; set; }

        public ICollection<Guid> Requirements { get; set; }

    }
}