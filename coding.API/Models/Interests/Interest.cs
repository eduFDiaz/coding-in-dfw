using System;
namespace coding.API.Models.Interests
{
    public class Interest : BaseEntity
    {
      
        public string Title { get; set; }
        public Guid UserId { get; set; }
    }
}