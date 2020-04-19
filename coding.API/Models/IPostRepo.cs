using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace coding.API.Models
{
    public interface IPostRepo
    {
        Task<Post> Create(Post post);
        Task<List<Post>> GetAllUserPost(int userid);
        Task<Post> GetPost(int postid);
        Task<bool> DeletePost(int postid);
        Task<bool> SaveAll();
    }
}