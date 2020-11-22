using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class CreateWorkExperienceDto
    {

        public string Title { get; set; }
        public string Resume { get; set; }
        public string Company { get; set; }
        public string DateRange { get; set; }
        public Guid UserId { get; set; }
       
    }
}