using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.FAQS
{
    [ExcludeFromCodeCoverage]
    public class FAQ : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }

    }
}