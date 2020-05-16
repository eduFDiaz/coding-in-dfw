

using System;

namespace coding.API.Dtos.Comments
{
    public class CommentForDetailDto
    {
        public Guid Id { get; set; }
        public string CommenterName { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public virtual DateTime DateCreated { get; set; }


        
    }
}