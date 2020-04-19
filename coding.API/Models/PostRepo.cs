using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

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

            // var user = _context.Users.FirstOrDefaultAsync(u => u.Id == userid);

            // if (user == null)
            //     return null;

            await _context.Posts.AddAsync(post);
            
            // user.Posts.AddAsync(post);

            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<List<Post>> GetPost(int userid)
        {
           var post = await _context.Posts.Where(p => p.UserId == userid).ToListAsync();
            
           return post;
        }

        public async Task<bool> DeletePost(int postid)
        {
            var postToDelete = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postid);

            if (postToDelete == null)
                return false;

            _context.Posts.Remove(postToDelete);
            
            await _context.SaveChangesAsync();

            return true;
        }                
    }
}