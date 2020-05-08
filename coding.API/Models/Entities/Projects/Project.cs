namespace coding.API.Models.Entities.Projects
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Resume { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
    }
}