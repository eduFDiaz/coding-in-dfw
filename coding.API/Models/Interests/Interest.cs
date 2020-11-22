using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Interests
{
    [ExcludeFromCodeCoverage]
    public class Interest : BaseEntity
    {
        public string Title { get; set; }
        public Guid UserId { get; set; }
    }
}