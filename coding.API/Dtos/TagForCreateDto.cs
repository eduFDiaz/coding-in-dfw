using System.ComponentModel.DataAnnotations;

namespace coding.API.Dtos
{
    public class TagForCreateDto
    {
        
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}