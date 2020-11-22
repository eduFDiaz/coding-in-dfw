using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Posts.Comments
{

    [ExcludeFromCodeCoverage]
    public class Comment : BaseEntity
    {
        [Required]
        public string Body { get; set; }
        public string CommenterName { get; set; }
        public string Email { get; set; }
        public Boolean Published { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }

    }
}