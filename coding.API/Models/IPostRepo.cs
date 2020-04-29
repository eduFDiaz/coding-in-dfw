using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using coding.API.Dtos;

namespace coding.API.Models
{
    public interface IPostRepo
    {
        Task<Post> Create(Post post);
        Task<List<Post>> GetAllUserPost(int userid);
        Task<Post> GetPost(int postid);
        Task<bool> DeletePost(int postid);
        Task<bool> SaveAll();
        Task<bool> AddTagsForPost(PostTag postTag);
        Task<Post> EditPost(int postid, PostForUpdateDto post);
        
               
    }
}