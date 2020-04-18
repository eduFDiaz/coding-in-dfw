using System.Threading.Tasks;
using System.Collections.Generic;

namespace coding.API.Models
{
    public interface IPostRepo
    {
        Task<Post> Create(Post post);
        Task<List<Post>> GetPost(int userid);
        // Task<IActionResult> GetPosts();
    }
}