using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models;


namespace coding.API.Dtos
{
    public class CreateSkillDto
    {

        public string Title { get; set; }
        public Guid UserId {get; set;}
       
    }
}