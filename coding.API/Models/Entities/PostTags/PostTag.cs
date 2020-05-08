using System.Collections.Generic;
using coding.API.Models.Entities.Posts;
using coding.API.Models.Entities.Tags;


namespace coding.API.Models.Entities.PostTags
{
    public class PostTag
    {
        
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
