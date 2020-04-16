using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models;

namespace coding.API.Dtos
{
    public class PostForCreateDto
    {
        public PostForCreateDto()
        {
            this.CreatedAt = DateTime.Now;
            // this.LastActive = DateTime.Now;
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public int ReadingTime { get; set; } // In minutes
        public ICollection<Tag> Tags { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        
        
        // [Required]
        // public string Username { get; set; }
        // [Required]
        // [DataType(DataType.Password)]
        // [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*?[#?!@$%^&*-])(?=.*[A-Z]).{8,20}$",
        // // [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*?[#?!@$%^&*-]).{8,20}$",
        // ErrorMessage = "Password must be at least 8 characters long, include one special character [#?!@$%^&*-], a capital letter and a number")]
        // public string Password { get; set; }
        // [Required]
        // public string FullName { get; set; }
        // [Required]
        // [DataType(DataType.EmailAddress)]
        // [EmailAddress]
        // public string Email { get; set; }
    
    }
}