
using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class FAQForDetailDto
    {
        public  Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
    }
}
