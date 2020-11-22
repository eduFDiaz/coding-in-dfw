using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos.Requirements
{
    [ExcludeFromCodeCoverage]
    public class RequirementForDetailDto
    {
        
        public Guid Id { get; set; }
        public string Description { get; set; }
        
        
    }
}