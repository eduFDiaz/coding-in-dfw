using System;
namespace coding.API.Models.Reviews

{
    public class Review : BaseEntity
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
        
    }
}
