using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coding.API.Models.Reviews;

namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class ReviewPresenter
    {
        private readonly Review _review;

        public ReviewPresenter(Review review)
        {
            _review = review;
        }

        [JsonProperty("id")]
        public Guid Id => _review.Id;

        [JsonProperty("company")]
        public string Company => _review.Company;

        [JsonProperty("name")]
        public string Name => _review.Name;


        [JsonProperty("email")]
        public string Email => _review.Email;

        [JsonProperty("body")]
        public string Body => _review.Body;

        [JsonProperty("status")]
        public string Status => _review.Status;

        [JsonProperty("url")]
        public string Url => _review.Url;

    }
}
