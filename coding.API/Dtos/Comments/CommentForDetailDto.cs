

using System;
using coding.API.Dtos.Posts;

namespace coding.API.Dtos.Comments
{
    public class CommentForDetailDto
    {
        public Guid Id { get; set; }
        public string CommenterName { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        // public Guid PostId { get; set; }
        public virtual DateTime DateCreated { get; set; }
        
        public PostInCommentDetailDto Post { get; set; }




    }
}