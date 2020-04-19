using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models;

namespace coding.API.Dtos
{
    public class TagForDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PostId { get; set; }
    }
}