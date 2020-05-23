using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using coding.API.Models.FAQS;

namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class FAQPresenter
    {
        private readonly FAQ _FAQ;

        public FAQPresenter(FAQ fAQ)
        {
            _FAQ = fAQ;

        }

        [JsonProperty("id")]
        public Guid Id => _FAQ.Id;

        [JsonProperty("title")]
        public string Title => _FAQ.Title;

        [JsonProperty("description")]
        public string Description => _FAQ.Description;

       
        

       
    }
}
