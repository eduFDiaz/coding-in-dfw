using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Languages
{
    [ExcludeFromCodeCoverage]
    public class Language : BaseEntity
    {
        
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}