using System;
using System.Diagnostics.CodeAnalysis;
using coding.API.Models.Tags;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class PostTagForCreateDto
    {
        public Guid PostId { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}