using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class UserForLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email {get; set; }
    }
}