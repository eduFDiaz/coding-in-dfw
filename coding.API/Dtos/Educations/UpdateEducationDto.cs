using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models;


namespace coding.API.Dtos
{
    public class UpdateEducationDto
    {

        public string Title { get; set; }
        public string SchoolName { get; set; }
        public string DateRange { get; set; }
       
        
    }
}