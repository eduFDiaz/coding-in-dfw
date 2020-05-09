using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models;


namespace coding.API.Dtos
{
    public class UpdateProjectDto
    {

        public string Title { get; set; }
        public string Resume { get; set; }
        public string Type { get; set; }
        
       
    }
}