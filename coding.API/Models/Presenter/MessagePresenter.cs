using Newtonsoft.Json;
using System;


using coding.API.Models.Messages;

namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Message baked to render.
    /// </summary>
    public class MessagePresenter
    {
        private readonly Message _message;

        public MessagePresenter(Message message)
        {
            _message = message;

        }

        [JsonProperty("id")]
        public Guid Id => _message.Id;

        [JsonProperty("name")]
        public string Name => _message.Name;

        [JsonProperty("email")]
        public string Email => _message.Email;

        [JsonProperty("serviceType")]
        public string serviceType => _message.ServiceType;

        [JsonProperty("text")]
        public string text => _message.Text;

       
    }
}
