using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models;

namespace coding.API.Dtos
{
    public class PostForUpdateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Text { get; set; }
        public int ReadingTime { get; set; } // In minutes
        public ICollection<Tag> Tags { get; set; }
                
    }
}