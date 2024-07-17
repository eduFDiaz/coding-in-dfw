using Newtonsoft.Json;
using System;

using coding.API.Models.Posts.Comments;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.Presenter
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Post baked to render.
    /// </summary>
    public class CommentPresenter
    {
        private readonly Comment _comment;

        public CommentPresenter(Comment comment)
        {
            _comment = comment;

        }

        [JsonProperty("id")]
        public Guid Id => _comment.Id;

        [JsonProperty("commenterName")]
        public string ComenterName => _comment.CommenterName;
        
        [JsonProperty("text")]
        public string Text => _comment.Body;

    }
}
