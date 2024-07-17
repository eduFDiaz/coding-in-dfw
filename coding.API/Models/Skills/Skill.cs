using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Skills
{
    [ExcludeFromCodeCoverage]
    public class Skill : BaseEntity
    {
        
        public string Title { get; set; }
        public Guid UserId {get; set;}
    }
}