namespace coding.API.Models.WorkExperiences
{
    public class WorkExperience
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Resume { get; set; }
        public string Company { get; set; }
        public string DateRange { get; set; }
        public int  UserId { get; set; }
        
    }
}