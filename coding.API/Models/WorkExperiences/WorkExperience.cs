using System;
namespace coding.API.Models.WorkExperiences
{
    public class WorkExperience: BaseEntity
    {
     
        public string Title { get; set; }
        public string Resume { get; set; }
        public string Company { get; set; }
        public string DateRange { get; set; }
        public Guid UserId { get; set; }
        
    }
}