using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class PostForCreateDto
    {
       
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Text { get; set; }
       
        public DateTime PublishedAt { get; set; }
        public int ReadingTime { get; set; } // In minutes
        public Guid UserId { get; set; }
        public ICollection<Guid> PostTagId { get; set; }
         
    }
}