using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models;

namespace coding.API.Dtos
{
    public class TagForUpdateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        
    }
}