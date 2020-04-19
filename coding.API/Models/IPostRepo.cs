using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace coding.API.Models
{
    public interface IPostRepo
    {
        Task<Post> Create(Post post);
        Task<List<Post>> GetPost(int userid);
        Task<bool> DeletePost(int postid);
    }
}