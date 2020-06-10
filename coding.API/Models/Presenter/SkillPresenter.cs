using Newtonsoft.Json;
using System;

using coding.API.Models.Skills;

namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class SkillPresenter
    {
        private readonly Skill _skill;

        public SkillPresenter(Skill skill)
        {
            _skill = skill;

        }

        [JsonProperty("id")]
        public Guid Id => _skill.Id;

        [JsonProperty("title")]
        public string Title => _skill.Title;
           
    }
}
