using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using coding.API.Models.Posts;
using coding.API.Models.Tags;
using coding.API.Models.Posts.Comments;

namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class PostPresenter
    {
        private readonly Post _post;


        public PostPresenter(Post post)
        {
            _post = post;
        }

        [JsonProperty("id")]
        public Guid Id => _post.Id;

        [JsonProperty("description")]
        public string Description => _post.Description;

        [JsonProperty("text")]
        public string Text => _post.Text;

        [JsonProperty("dateCreated")]
        public DateTime DateCreated => _post.DateCreated;

        [JsonProperty("readingTime")]
        public int ReadingTime => _post.ReadingTime;


        [JsonProperty("tags")]
        // public ICollection<Tag> Tags => _post.PostTags.Select(t => t.Tag).ToList();
        public IEnumerable<Object> Tags => _post.PostTags.Select(p => new
        {
            Id = p.Tag.Id,
            Description = p.Tag.Description,
            Title = p.Tag.Title
        }).ToList();

        [JsonProperty("photourl")]
        public string PhotoUrl => _post.Photos.Where(p => p.IsMain == true).Select(p => p.Url).SingleOrDefault();

        [JsonProperty("comments")]
        public IEnumerable<Object> comments => _post.Comments.
        Select(c => new
        {
            Id = c.Id,
            CommenterName = c.CommenterName,
            Body = c.Body,
            Email = c.Email,
            Published = c.Published
        }).
        Where(c => c.Published == true).ToList();




    }
}
