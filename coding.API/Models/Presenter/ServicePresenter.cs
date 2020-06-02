using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using coding.API.Models.Awards;
using coding.API.Models.Services;

namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class ServicePresenter
    {
        private readonly Service _service;

        public ServicePresenter(Service service)
        {
            _service = service;

        }

        [JsonProperty("id")]
        public Guid Id => _service.Id;

        [JsonProperty("body")]
        public string Body => _service.Body;

    }
}
