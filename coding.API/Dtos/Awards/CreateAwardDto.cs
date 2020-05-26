using System;


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