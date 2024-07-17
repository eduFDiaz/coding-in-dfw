using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;


namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class PhotoForCreationDto
    {

        public PhotoForCreationDto()
        {
            this.DateAdded = DateTime.Now;
        }

        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public Guid UserId {get; set;}

        
    }
}