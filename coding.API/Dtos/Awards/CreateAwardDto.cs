using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models;


namespace coding.API.Dtos
{
    public class CreateAwardDto
    {

         public string Title { get; set; }
        public string Company { get; set; }
        public int Year { get; set; }
        public Guid UserId { get; set; }
        
    }
}