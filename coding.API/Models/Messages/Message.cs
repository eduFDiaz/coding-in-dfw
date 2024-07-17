
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Messages {
        [ExcludeFromCodeCoverage]
        public class Message: BaseEntity {
            
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public string ServiceType { get; set; }
        public string Text { get; set; }

        public bool isRead {get; set;}

        }

    }     
        
        
