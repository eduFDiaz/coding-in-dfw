using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos.Reviews
{
    [ExcludeFromCodeCoverage]

    public class DraftReviewDto
    {
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public string Company { get; set; }

    }

}
