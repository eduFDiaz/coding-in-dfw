using System;
using coding.API.Models;
using coding.API.Models.Users;

namespace coding.API.Models.Photos
{
    public class Photo  : BaseEntity
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        // This is how is defined that a photo belongs to an user
        // see user model (public ICollection<Photo> Photos { get; set; })
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string  PublicId { get; set; }
    }
}