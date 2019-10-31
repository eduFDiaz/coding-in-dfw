using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace coding.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8}$",
        ErrorMessage = "Password must be at least 8 characters long, include one special character, a capital letter and a number")]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
    }
}