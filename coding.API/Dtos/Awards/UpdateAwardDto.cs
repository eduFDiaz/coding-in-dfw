using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models;


namespace coding.API.Dtos
{
    public class UpdateAwardDto
    {

        public string Title { get; set; }
        public string Company { get; set; }
        public int Year { get; set; }
               
    }
}