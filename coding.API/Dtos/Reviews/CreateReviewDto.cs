using System;

namespace coding.API.Dtos.Reviews
{

    public class CreateReviewDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }

    }

}