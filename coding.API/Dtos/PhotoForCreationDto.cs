using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models;


namespace coding.API.Dtos
{
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
        
    }
}