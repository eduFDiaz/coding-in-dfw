namespace coding.API.Models.Entities.Awards
{
    public class Award
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public int Year { get; set; }
        public int UserId { get; set; }
    }
}