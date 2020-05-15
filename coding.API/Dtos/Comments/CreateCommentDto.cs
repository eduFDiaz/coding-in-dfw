using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using coding.API.Models;


namespace coding.API.Dtos.Comments
{
    public class CreateCommentDto
    {

        public CreateCommentDto()
        {
            this.Published = false;
            
        }

        public string Body { get; set; }
        public string CommenterName { get; set; }
        public Guid PostId { get; set; }   
        public Boolean Published { get; set; }
        
    }
}