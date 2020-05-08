using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using coding.API.Dtos;
using coding.API.Models.Entities.Posts;
using coding.API.Models.Entities.PostTags;

namespace coding.API.Models.Interfaces
{
    public interface IPostRepo
    {
        Task<Post> Create(Post post);
        Task<List<Post>> GetAllUserPost(int userid);
        Task<Post> GetPost(int postid);
        Task<bool> DeletePost(int postid);
        Task<bool> SaveAll();
        Task<bool> AddTagsForPost(PostTag postTag);
        Task<bool> UpdateTagsForPost(PostTag postTag);
        Task<PostTag> GetTagsForPost(int postid);
        Task<Post> EditPost(int postid, PostForUpdateDto post);
        
               
    }
}