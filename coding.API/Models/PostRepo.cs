using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace coding.API.Models
{
    public class PostRepo : IPostRepo
    {
        
        private readonly DataContext _context;
        public PostRepo(DataContext context)
        {
            _context = context;
        }
        public async Task<Post> Create(Post post)
        {
            if (post == null)
                return null;

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }
        
        
    }
}