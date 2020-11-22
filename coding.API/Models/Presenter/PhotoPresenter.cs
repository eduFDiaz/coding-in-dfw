using Newtonsoft.Json;
using System;


using coding.API.Models.Photos;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Presenter
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class PhotoPresenter
    {
        private readonly Photo _photo;

        public PhotoPresenter(Photo photo)
        {
            _photo = photo;

        }

        [JsonProperty("id")]
        public Guid Id => _photo.Id;

        [JsonProperty("url")]
        public string Url => _photo.Url;

        [JsonProperty("description")]
        public string Description => _photo.Description;
        
        [JsonProperty("dateAdded")]
        public DateTime DateAdded => _photo.DateAdded;

        [JsonProperty("isMain")]
        public bool IsMain => _photo.IsMain;

        [JsonProperty("publicId")]
        public string  PublicId => _photo.PublicId;

        
    }
}
