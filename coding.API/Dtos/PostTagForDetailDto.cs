using System;
using coding.API.Models;

namespace coding.API.Dtos
{
    public class PostTagForDetailDto
    {
        // public int PostId { get; set; }
        public Guid TagId { get; set; } 
        public TagForDetailDto Tag { get; set; }
    }
}