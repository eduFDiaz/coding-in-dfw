using System;
using coding.API.Models.Users;

namespace coding.API.Models.Photos
{
    public class Photo  : BaseEntity
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string  PublicId { get; set; }
    }
}