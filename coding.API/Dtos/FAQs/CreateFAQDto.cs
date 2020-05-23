using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models;


namespace coding.API.Dtos
{
    public class CreateFAQDto
    {

        public string Title { get; set; }
        public string Description { get; set; }
        
    }
}