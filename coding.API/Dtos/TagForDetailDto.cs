using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    public class TagForDetailDto
    {
        [ExcludeFromCodeCoverage]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}