using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Awards
{
    [ExcludeFromCodeCoverage]
    public class Award : BaseEntity
    {
      
        public string Title { get; set; }
        public string Company { get; set; }
        public int Year { get; set; }
        public Guid UserId { get; set; }
    }
}