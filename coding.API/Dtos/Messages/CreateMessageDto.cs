using System;
using System.ComponentModel.DataAnnotations;

namespace coding.API.Dtos
{
    public class CreateMessageDto
    {
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public string ServiceType { get; set; }
        public string Text { get; set; }
        
    }
}