using System;
using System.Collections.Generic;
namespace coding.API.Dtos
{
    public class UserForUpdateDto
    {
        public string Username { get; set; }
        public string CustomUserTitle { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string ShortResume { get; set; }
        public string FullResume { get; set; }
        public string GithubUrl { get; set; }
        public string TwiterProfile { get; set; }
        public string FacebookProfile { get; set; }
        public string LinkedInProfile { get; set; }
        public string StackOverflowProfile { get; set; }
        public string RedditProfile { get; set; }
        public string CodepenProfile { get; set; }

        public string ServiceAndPricingTable { get; set; }


    }
}