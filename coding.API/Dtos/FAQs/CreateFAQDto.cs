using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class CreateFAQDto
    {

        public string Title { get; set; }
        public string Description { get; set; }

        public Guid UserId { get; set; }


    }
}