using System;
using System.Collections.Generic;

namespace coding.API.Dtos
{
    public class UserForRegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}