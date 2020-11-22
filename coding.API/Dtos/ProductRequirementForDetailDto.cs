using System.Diagnostics.CodeAnalysis;
using coding.API.Dtos.Requirements;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class ProductRequirementForDetailDto
    {
        public  RequirementForDetailDto Requirement { get; set; }
    }
}