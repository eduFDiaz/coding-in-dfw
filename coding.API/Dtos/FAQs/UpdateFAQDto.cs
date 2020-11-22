using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class UpdateFAQDto
    {

        public string Title { get; set; }
        public string Description { get; set; }
        
    }
}