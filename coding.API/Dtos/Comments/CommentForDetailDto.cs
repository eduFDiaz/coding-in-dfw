

using System;
using System.Diagnostics.CodeAnalysis;
using coding.API.Dtos.Posts;

namespace coding.API.Dtos.Comments
{
    [ExcludeFromCodeCoverage]
    public class CommentForDetailDto
    {
        public Guid Id { get; set; }
        public string CommenterName { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public  DateTime DateCreated { get; set; }
        
        public PostInCommentDetailDto Post { get; set; }




    }
}