using Newtonsoft.Json;
using System;


using coding.API.Models.Awards;

namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class AwardPresenter
    {
        private readonly Award _award;

        public AwardPresenter(Award award)
        {
            _award = award;

        }

        [JsonProperty("id")]
        public Guid Id => _award.Id;

        [JsonProperty("company")]
        public string Company => _award.Company;
        
        [JsonProperty("year")]
        public int Year => _award.Year;


        [JsonProperty("title")]
        public string Title => _award.Title;

       
        

       
    }
}
