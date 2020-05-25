using System;

namespace coding.API.Dtos.Posts
{
    public class PostPhotoForDetailDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
    }
}