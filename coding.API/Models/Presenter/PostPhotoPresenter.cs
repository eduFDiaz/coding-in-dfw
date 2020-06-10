﻿using Newtonsoft.Json;
using System;

using coding.API.Models.Photos;

namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class PostPhotoPresenter
    {
        private readonly PostPhoto _photo;

        public PostPhotoPresenter(PostPhoto photo)
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
        public string PublicId => _photo.PublicId;


    }
}
