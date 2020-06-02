using System;
using System.ComponentModel.DataAnnotations;

namespace coding.API.Dtos.Services
{
    public class CreateServiceDto
    {


        public string Body { get; set; }

        public Guid UserId { get; set; }

    }
}