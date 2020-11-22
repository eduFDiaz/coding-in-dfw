using Newtonsoft.Json;
using System;


using coding.API.Models.Interests;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Presenter
{
    [ExcludeFromCodeCoverage]
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
