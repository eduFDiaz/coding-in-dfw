using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace coding.API.Models.Posts.Comments
{
    public class Comment: BaseEntity
    {
        [Required]
        public string Body { get; set; }
        public string CommenterName { get; set; }
        public Boolean Published { get; set; }  
        public Guid PostId { get; set; }
        public Post Posts { get; set; }

    }
}