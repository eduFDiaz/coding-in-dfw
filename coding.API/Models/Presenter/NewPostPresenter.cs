using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


using coding.API.Models.Posts;


namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class NewPostPresenter
    {
        private readonly Post _post;


        public NewPostPresenter(Post post)
        {
            _post = post;
        }
        
        [JsonProperty("id")]
        public Guid Id => _post.Id;

        [JsonProperty("title")]
        public string Title => _post.Title; 

        [JsonProperty("description")]
        public string Description => _post.Description;

        [JsonProperty("text")]
        public string Text => _post.Text;

        [JsonProperty("dateCreated")]
        public DateTime DateCreated => _post.DateCreated;

        [JsonProperty("readingTime")]
        public int ReadingTime => _post.ReadingTime;


        [JsonProperty("tags")]
        public IEnumerable<Object> Tags => _post.PostTags.Select(p => new
        {
            Id = p.Tag.Id,
            Description = p.Tag.Description,
            Title = p.Tag.Title
        }).ToList();




    }
}
