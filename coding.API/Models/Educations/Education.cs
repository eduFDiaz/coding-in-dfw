using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Educations
{
    [ExcludeFromCodeCoverage]
    public class Education : BaseEntity
    {
        
        public string Title { get; set; }
        public string SchoolName { get; set; }
        public string DateRange { get; set; }
        public Guid UserId { get; set; }
    }
}