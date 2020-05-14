using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using coding.API.Models.Interests;

namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class InterestPresenter
    {
        private readonly Interest _interest;

        public InterestPresenter(Interest interest)
        {
            _interest = interest;

        }

        [JsonProperty("id")]
        public Guid Id => _interest.Id;

        [JsonProperty("title")]
        public string Title => _interest.Title;

       
        

       
    }
}
