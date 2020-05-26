using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Dtos.Posts;

namespace coding.API.Dtos
{
    public class PostForDetailDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public List<PostPhotoForDetailDto> Photos { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public int ReadingTime { get; set; } // In minutes
        // public ICollection<Tag> Tags { get; set; }
        // public User User { get; set; }
        public Guid UserId { get; set; }
        // public List<PostTag> PostTags { get; set; }
        public List<PostTagForDetailDto> PostTags { get; set; }
    }
}