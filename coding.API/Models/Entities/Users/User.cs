using System;
using System.Collections.Generic;

using coding.API.Models.Entities.WorkExperiences;
using coding.API.Models.Entities.Skills;
using coding.API.Models.Entities.Educations;
using coding.API.Models.Entities.Awards;
using coding.API.Models.Entities.Languages;
using coding.API.Models.Entities.Projects;
using coding.API.Models.Entities.Posts;
using coding.API.Models.Entities.Photos;
using coding.API.Models.Entities.Products;


namespace coding.API.Models.Entities.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
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
        public ICollection<Skill> Skills {get; set; }
        public ICollection<Education> Educations {get; set; }
        public ICollection<Award> Awards { get; set; }
        public ICollection<Language> Languages { get; set; }
        public ICollection<Project> Projects { get; set; }
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