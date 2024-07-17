using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos.Posts
{
    [ExcludeFromCodeCoverage]
    public class PostPhotoForDetailDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
    }
}