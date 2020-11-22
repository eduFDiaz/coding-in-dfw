using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class CreateProjectDto
    {

        public string Title { get; set; }
        public string Resume { get; set; }
        public string Type { get; set; }
        public Guid UserId { get; set; }
       
    }
}