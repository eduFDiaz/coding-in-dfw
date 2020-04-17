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

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<List<Post>> GetPost()
        {
        //    var post = await _context.Posts.Where(p => p.UserId == id).FirstOrDefaultAsync();
            var post = await this._context.Posts.ToListAsync();

            return post;
        }

        //  public async Task<List<User>> GetUsers()
        // {
        //     var users = await this._context.Users.ToListAsync();
        //     return users;
        //     // return this.users;
        // }
        
        
    }
}