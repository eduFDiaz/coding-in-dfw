using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos.Requirements
{
    public class RequirementForDetailDto
    {
        [ExcludeFromCodeCoverage]
        public Guid Id { get; set; }
        public string Description { get; set; }
        
        
    }
}