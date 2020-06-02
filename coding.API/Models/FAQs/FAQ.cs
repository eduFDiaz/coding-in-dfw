using System;

namespace coding.API.Models.FAQS
{
    public class FAQ : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }

    }
}