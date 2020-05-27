using System;


namespace coding.API.Dtos
{
    public class CreateFAQDto
    {

        public string Title { get; set; }
        public string Description { get; set; }

        public Guid UserId { get; set; }


    }
}