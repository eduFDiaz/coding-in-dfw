using System;
namespace coding.API.Models.Services
{
    public class Service : BaseEntity
    {

        public string Body { get; set; }
        public Guid UserId { get; set; }
    }
}