using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class CreateLanguageDto
    {

        public string Name { get; set; }
        public Guid UserId {get; set;}
        
    }
}