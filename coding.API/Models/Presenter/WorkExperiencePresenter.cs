using Newtonsoft.Json;
using System;


using coding.API.Models.WorkExperiences;

namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class WorkExperiencePresenter
    {
        private readonly WorkExperience _workExperience;

        public WorkExperiencePresenter(WorkExperience workExperience)
        {
            _workExperience = workExperience;

        }

        [JsonProperty("id")]
        public Guid Id => _workExperience.Id;


        [JsonProperty("resume")]   
        public string Resume => _workExperience.Resume;
                

        [JsonProperty("company")]
        public string Company => _workExperience.Company;
        
        [JsonProperty("dateRange")]
        public string DateRange => _workExperience.DateRange;


        [JsonProperty("title")]
        public string Title => _workExperience.Title;
        

       
    }
}
