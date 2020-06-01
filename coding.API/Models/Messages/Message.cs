using System;
using System.ComponentModel.DataAnnotations;

namespace coding.API.Models.Messages {
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
        
        
