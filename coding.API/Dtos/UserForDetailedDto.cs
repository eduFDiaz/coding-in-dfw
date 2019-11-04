using System;
using System.Collections.Generic;
using coding.API.Models;

namespace coding.API.Dtos
{
    public class UserForDetailedDto
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        // One to Many relationship
        public ICollection<PostForDetailDto> Posts { get; set; }
        public ICollection<PhotoForDetailDto> Photos { get; set; }
    }
}