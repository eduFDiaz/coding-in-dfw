using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class UpdateProjectDto
    {

        public string Title { get; set; }
        public string Resume { get; set; }
        public string Type { get; set; }
        
       
    }
}