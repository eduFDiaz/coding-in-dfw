
using System;
using coding.API.Models.Products.Requirements;
using Newtonsoft.Json;

namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Product baked to render.
    /// </summary>
    public class RequirementPresenter
    {
        private readonly Requirement _requirement;


        public RequirementPresenter(Requirement requirement)
        {
            _requirement = requirement;
        }

        [JsonProperty("id")]
        public Guid Id => _requirement.Id;

        [JsonProperty("description")]
        public string Description => _requirement.Description;

                       
    }
}
