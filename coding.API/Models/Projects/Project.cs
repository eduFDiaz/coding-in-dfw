using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Projects
{
    [ExcludeFromCodeCoverage]
    public class Project : BaseEntity
    {
        
        public string Title { get; set; }
        public string Resume { get; set; }
        public string Type { get; set; }
        public Guid UserId { get; set; }
    }
}