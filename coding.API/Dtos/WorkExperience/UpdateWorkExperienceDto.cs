
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class UpdateWorkExperienceDto
    {

        public string Title { get; set; }
        public string Resume { get; set; }
        public string Company { get; set; }
        public string DateRange { get; set; }
               
       
    }
}