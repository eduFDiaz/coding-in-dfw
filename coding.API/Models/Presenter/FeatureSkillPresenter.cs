using Newtonsoft.Json;
using System;

using coding.API.Models.FeatureSkills;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Presenter
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class FeatureSkillPresenter
    {
        private readonly FeatureSkill _featureSkill;

        public FeatureSkillPresenter(FeatureSkill featureSkill)
        {
            _featureSkill = featureSkill;
        }

        [JsonProperty("id")]
        public Guid Id => _featureSkill.Id;

        [JsonProperty("title")]
        public string Name => _featureSkill.Title;

        [JsonProperty("body")]
        public string Body => _featureSkill.Body;

        [JsonProperty("icons")]
        public string[] Status => _featureSkill.Icons;


    }
}
