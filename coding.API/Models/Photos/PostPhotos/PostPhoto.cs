using System;
using coding.API.Models.Posts;

namespace coding.API.Models.Photos
{
    public class PostPhoto  : BaseEntity
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        // This is how is defined that a photo belongs to an user
        // see user model (public ICollection<Photo> Photos { get; set; })
        public Post Post { get; set; }
        public Guid PostId { get; set; }
        public string  PublicId { get; set; }
    }
}