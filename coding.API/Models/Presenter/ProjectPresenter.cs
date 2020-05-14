using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using coding.API.Models.Projects;

namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class ProjectPresenter
    {
        private readonly Project _project;

        public ProjectPresenter(Project project)
        {
            _project = project;

        }

        [JsonProperty("id")]
        public Guid Id => _project.Id;

        [JsonProperty("title")]
        public string Title => _project.Title;

        [JsonProperty("resume")]
        public string Resume => _project.Resume;

        [JsonProperty("type")]
        public string Type => _project.Type;

    }
}
