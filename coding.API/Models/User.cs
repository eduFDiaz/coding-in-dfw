using System;
using System.Collections.Generic;

namespace coding.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        // One to Many relationship
        public ICollection<Post> Posts { get; set; }
        public ICollection<Photo> Photos { get; set; }
        // Many to Many relationship
        // public ICollection<Like> Likers { get; set; }
        // public ICollection<Like> Likeers { get; set; }
        // public ICollection<Message> MessagesSent { get; set; }
        // public ICollection<Message> MessagesReceived { get; set; }
        public string Phone { get; set; }
        
    }
}