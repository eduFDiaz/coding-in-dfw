using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace coding.API.Dtos
{
    public class UserForRegisterDto
    {
        public UserForRegisterDto()
        {
            this.Created = DateTime.Now;
            this.LastActive = DateTime.Now;
        }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*?[#?!@$%^&*-])(?=.*[A-Z]).{8,20}$",
        // [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*?[#?!@$%^&*-]).{8,20}$",
        ErrorMessage = "Password must be at least 8 characters long, include one special character [#?!@$%^&*-], a capital letter and a number")]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
    }
}