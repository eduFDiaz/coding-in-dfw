using System.Collections.Generic;
using coding.API.Models.Entities.PostTags;

namespace coding.API.Models.Entities.Tags
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
        
    }
}