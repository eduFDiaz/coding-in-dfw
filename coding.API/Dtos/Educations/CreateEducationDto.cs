using System;


namespace coding.API.Dtos
{
    public class CreateEducationDto
    {

        public string Title { get; set; }
        public string SchoolName { get; set; }
        public string DateRange { get; set; }
        public Guid UserId { get; set; }
        
    }
}