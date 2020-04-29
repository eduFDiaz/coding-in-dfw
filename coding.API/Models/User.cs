using System;
using System.Collections.Generic;
using coding.API.Models.Products;
using coding.API.Models.WorkExperiences;

namespace coding.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        // public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string CurrentOcupation { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        // One to Many relationship
        public ICollection<Post> Posts { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<WorkExperience> WorkExperiences { get; set; }
        // Many to Many relationship
        // public ICollection<Like> Likers { get; set; }
        // public ICollection<Like> Likeers { get; set; }
        // public ICollection<Message> MessagesSent { get; set; }
        // public ICollection<Message> MessagesReceived { get; set; }
        public string Phone { get; set; }
        public string ShortResume { get; set; }
        public string FullResume { get; set; }
        public string GithubUrl { get; set; }
        public string TwiterProfile { get; set; }
        public string FacebookProfile { get; set; }
        public string LinkedInProfile { get; set; }
        public string StackOverflowProfile { get; set; }
        public string RedditProfile { get; set; }
        public string CodepenProfile { get; set; }
        
    }
}