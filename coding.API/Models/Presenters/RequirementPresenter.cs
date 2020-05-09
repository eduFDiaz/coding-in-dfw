
using coding.API.Models.Entities.Products.Requirements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public int Id => _requirement.Id;

        [JsonProperty("description")]
        public string Description => _requirement.Description;

                       
    }
}
