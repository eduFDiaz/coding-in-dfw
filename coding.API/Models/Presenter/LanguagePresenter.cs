﻿using Newtonsoft.Json;
using System;


using coding.API.Models.Languages;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Presenter
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class LanguagePresenter
    {
        private readonly Language _language;

        public LanguagePresenter(Language language)
        {
            _language = language;

        }

        [JsonProperty("id")]
        public Guid Id => _language.Id;

        [JsonProperty("name")]
        public string Name => _language.Name;

       
    }
}
