using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Dtos.Comments;

namespace coding.API.Dtos.Posts
{
    public class PostAllCommentDetailDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public int ReadingTime { get; set; }
        
        public List<CommentClearForDetailDto> Comments { get; set; } 
        // In minutes
        // public ICollection<Tag> Tags { get; set; }
        // public User User { get; set; }
      //  public int UserId { get; set; }
        // public List<PostTag> PostTags { get; set; }
        public List<PostTagForDetailDto> PostTags { get; set; }

        


    }
}