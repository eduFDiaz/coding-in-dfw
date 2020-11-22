using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class TagForCreateDto
    {
        
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}