namespace coding.API.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Photo Photo { get; set; }
    }
}