

using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos.Comments
{
    [ExcludeFromCodeCoverage]
    public class CommentClearForDetailDto
    {
        public Guid Id { get; set; }
        public string CommenterName { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }

        public virtual DateTime DateCreated { get; set; }
        




    }
}