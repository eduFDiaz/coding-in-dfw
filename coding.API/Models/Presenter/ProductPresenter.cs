using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

using coding.API.Models.Products;
using coding.API.Models.Products.Requirements;

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

        [JsonProperty("bodyText")]
        public string BodyText => _product.BodyText;

        [JsonProperty("requirements")]
        public IEnumerable<Object> Requirement => _product.ProductRequirements.Select(p => new
        {
            Id = p.Requirement.Id,
            Description = p.Requirement.Description
        }).ToList();

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


        [JsonProperty("photourl")]
        public string PhotoUrl => _product.Photos.Where(p => p.IsMain == true).Select(p => p.Url).SingleOrDefault();

        [JsonProperty("description")]
        public string ProductDescription => _product.ProductDescription;

        [JsonProperty("projectIntro")]
        public string ProjectIntro => _product.ProjectIntro;

        [JsonProperty("shortResume")]
        public string ShortResume => _product.ShortResume;

        [JsonProperty("clientName")]
        public string ClientName => _product.ClientName;

    }
}
