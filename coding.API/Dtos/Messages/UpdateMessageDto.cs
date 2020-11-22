using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class UpdateMessageDto
    {
        public bool isRead { get; set; }
        
    }
}