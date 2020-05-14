using System;

namespace coding.API.Models.Languages
{
    public class Language : BaseEntity
    {
        
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}