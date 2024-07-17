using System;
using System.Diagnostics.CodeAnalysis;
using coding.API.Models.Products.Requirements;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class ProductRequirementForCreateDto
    {
        public Guid ProductId { get; set; }
        public Guid RequirementId { get; set; }

        public Requirement Requirement { get; set; }
    }
}