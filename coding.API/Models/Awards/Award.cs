using System;
namespace coding.API.Models.Awards
{
    public class Award : BaseEntity
    {
      
        public string Title { get; set; }
        public string Company { get; set; }
        public int Year { get; set; }
        public Guid UserId { get; set; }
    }
}