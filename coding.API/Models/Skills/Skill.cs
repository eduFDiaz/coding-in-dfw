using System;
namespace coding.API.Models.Skills
{
    public class Skill : BaseEntity
    {
        
        public string Title { get; set; }
        public Guid UserId {get; set;}
    }
}