using System;


namespace coding.API.Dtos
{
    public class CreateSkillDto
    {

        public string Title { get; set; }
        public Guid UserId {get; set;}
       
    }
}