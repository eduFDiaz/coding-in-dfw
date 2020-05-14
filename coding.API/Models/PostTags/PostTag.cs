using System;
using coding.API.Models.Posts;
using coding.API.Models.Tags;

namespace coding.API.Models.PostTags
{
    public class PostTag: BaseEntity
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}