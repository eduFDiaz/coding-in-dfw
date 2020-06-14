using System;
using System.ComponentModel.DataAnnotations;

namespace coding.API.Models.Subscribers
{
    public class Subscriber : BaseEntity
    {
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

    }
}