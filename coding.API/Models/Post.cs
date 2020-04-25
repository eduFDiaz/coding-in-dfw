using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using coding.API.Models;
namespace coding.API.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int ReadingTime { get; set; } // In minutes
        public ICollection<PostTag> PostTags { get; set; }
        // public User User { get; set; }
        public int UserId { get; set; }
    }
}