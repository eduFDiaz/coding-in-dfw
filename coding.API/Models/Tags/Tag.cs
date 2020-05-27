using System;
using System.Collections.Generic;
using coding.API.Models.PostTags;

namespace coding.API.Models.Tags
{
    public class Tag: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<PostTag> PostTags { get; set; }


    }
}