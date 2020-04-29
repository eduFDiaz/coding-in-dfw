namespace coding.API.Models.Educations
{
    public class Education
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SchoolName { get; set; }
        public string DateRange { get; set; }
        public int UserId { get; set; }
    }
}