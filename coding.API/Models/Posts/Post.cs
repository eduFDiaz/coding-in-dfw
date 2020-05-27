using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models.PostTags;
using coding.API.Models.Posts.Comments;
using coding.API.Models.Photos;

namespace coding.API.Models.Posts
{
    public class Post: BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int ReadingTime { get; set; } // In minutes
        public List<PostTag> PostTags { get; set; }
        public List<PostPhoto> Photos { get; set; }
        public Guid UserId { get; set; }       
        public List<Comment> Comments { get; set; }
    }
} 