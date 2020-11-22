using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Subscribers
{
    [ExcludeFromCodeCoverage]
    public class Subscriber : BaseEntity
    {
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

    }
}