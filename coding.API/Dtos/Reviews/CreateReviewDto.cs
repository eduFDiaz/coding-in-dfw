using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos.Reviews
{
    [ExcludeFromCodeCoverage]

    public class CreateReviewDto
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Body { get; set; }
    

    }

}
