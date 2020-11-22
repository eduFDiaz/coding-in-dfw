using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class CreateInterestDto
    {

         public string Title { get; set; }
        public Guid UserId { get; set; }
        
    }
}