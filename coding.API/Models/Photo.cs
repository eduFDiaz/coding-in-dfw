using System;

namespace coding.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        // This is how is defined that a photo belongs to an user
        // see user model (public ICollection<Photo> Photos { get; set; })
        public int UserId { get; set; }
        public string  PublicId { get; set; }
    }
}