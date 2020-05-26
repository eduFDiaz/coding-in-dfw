using System;


namespace coding.API.Dtos
{
    public class CreateProjectDto
    {

        public string Title { get; set; }
        public string Resume { get; set; }
        public string Type { get; set; }
        public Guid UserId { get; set; }
       
    }
}