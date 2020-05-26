using System;
using coding.API.Models;

namespace coding.API.Dtos
{
    public class PostTagForDetailDto
    {
        public Guid TagId { get; set; } 
        public TagForDetailDto Tag { get; set; }
    }
}