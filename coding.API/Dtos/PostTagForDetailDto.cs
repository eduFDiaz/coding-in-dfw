using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class PostTagForDetailDto
    {
        public Guid TagId { get; set; } 
        public TagForDetailDto Tag { get; set; }
    }
}