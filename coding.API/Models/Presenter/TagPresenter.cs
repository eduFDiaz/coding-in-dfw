using Newtonsoft.Json;
using System;


using coding.API.Models.Tags;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Presenter
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class TagPresenter
    {
        private readonly Tag _tag;

        public TagPresenter(Tag tag)
        {
            _tag = tag;

        }

        [JsonProperty("id")]
        public Guid Id => _tag.Id;

        [JsonProperty("description")]
        public string Description => _tag.Description;

        [JsonProperty("title")]
        public string Title => _tag.Title;

    }
}
