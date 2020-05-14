using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

using coding.API.Models.Products;


namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class ProductPresenter
    {
        private readonly Product _product;

        public ProductPresenter(Product product)
        {
            _product = product;

        }

        [JsonProperty("id")]
        public Guid Id => _product.Id;

        [JsonProperty("text")]
        public string Description => _product.BodyText;

        // [JsonProperty("requirements")]
        // public ICollection<int> Requirements => _product.ProductRequirements.Select(t => t.RequirementId).ToList();

        [JsonProperty("requirements")]
        public ICollection<string> Requirements => _product.ProductRequirements.Select(t => t.Requirement).Select(t => t.Description).ToList();

        [JsonProperty("industry")]
        public string Industry => _product.Industry;

        [JsonProperty("size")]
        public int Size => _product.Size;

        [JsonProperty("name")]
        public string Name => _product.Name;

        [JsonProperty("type")]
        public string Type => _product.Type;

        [JsonProperty("url")]
        public string Url => _product.Url;

        [JsonProperty("photoUrl")]
        public string ProductPhoto => _product.ProductPhoto;

        [JsonProperty("description")]
        public string ProductDescription => _product.ProductDescription;

        [JsonProperty("intro")]
        public string ProjectIntro => _product.ProjectIntro;

        [JsonProperty("shortResume")]
        public string ShortResume => _product.ShortResume;

        [JsonProperty("clientName")]
        public string ClientName => _product.ClientName;

    }
}
