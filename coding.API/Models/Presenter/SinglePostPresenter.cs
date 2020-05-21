using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using coding.API.Models.Posts;
using coding.API.Models.Tags;
using coding.API.Models.PostTags;
using coding.API.Models.Posts.Comments;

namespace coding.API.Models.Presenter
{
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class SinglePostPresenter
    {
        private readonly Post _post;


        public SinglePostPresenter(Post post)
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

        // haz un resumen de los tag
        [JsonProperty("tags")]
        public List<String> Tags => _post.PostTags.Select(t => t.Tag.Title).ToList();

        [JsonProperty("comments")]
        public ICollection<Comment> Comments =>  _post.Comments.ToList();

        


    }
}
