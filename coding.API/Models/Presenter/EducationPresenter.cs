using Newtonsoft.Json;
using System;


using coding.API.Models.Educations;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Presenter
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class EducationPresenter
    {
        private readonly Education _education;

        public EducationPresenter(Education education)
        {
            _education = education;

        }

        [JsonProperty("id")]
        public Guid Id => _education.Id;

        [JsonProperty("title")]
        public string Title => _education.Title;

        [JsonProperty("schoolName")]
        public string SchoolName => _education.SchoolName;

        [JsonProperty("dateRange")]
         public string DateRange => _education.DateRange;

        

       
    }
}
