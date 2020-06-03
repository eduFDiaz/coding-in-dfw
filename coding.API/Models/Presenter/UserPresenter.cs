using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using coding.API.Models.Users;

namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class UserPresenter
    {
        private readonly User _user;

        public UserPresenter(User user)
        {
            _user = user;
        }

        [JsonProperty("id")]
        public Guid Id => _user.Id;

        [JsonProperty("username")]
        public string Username => _user.Username;

        [JsonProperty("fullname")]
        public string FullName => _user.FullName;

        [JsonProperty("email")]
        public string Email => _user.Email;

        [JsonProperty("location")]
        public string Location => _user.Location;

        [JsonProperty("created")]
        public DateTime Created => _user.Created;

        [JsonProperty("lastActive")]
        public DateTime LastActive => _user.LastActive;

        [JsonProperty("phone")]
        public string Phone => _user.Phone;

        [JsonProperty("shortResume")]
        public string ShortResume => _user.ShortResume;

        [JsonProperty("fullResume")]
        public string FullResume => _user.FullResume;

        [JsonProperty("githubUrl")]
        public string GithubUrl => _user.GithubUrl;

        [JsonProperty("twitterProfile")]
        public string TwiterProfile => _user.TwiterProfile;

        [JsonProperty("facebookProfile")]
        public string FacebookProfile => _user.FacebookProfile;

        [JsonProperty("linkediInProfile")]
        public string LinkedInProfile => _user.LinkedInProfile;

        [JsonProperty("stackOverflowProfile")]
        public string StackOverflowProfile => _user.StackOverflowProfile;

        [JsonProperty("redditProfile")]
        public string RedditProfile => _user.RedditProfile;

        [JsonProperty("codepenProfile")]
        public string CodepenProfile => _user.CodepenProfile;

        //     [JsonProperty("photoUrl")]
        //     public string PhotoUrl => {

        //         if ()
        // };




    }
}
