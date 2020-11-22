using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using coding.API.Models.PostTags;

namespace coding.API.Models.Tags
{
    [ExcludeFromCodeCoverage]
    public class Tag: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<PostTag> PostTags { get; set; }


    }
}