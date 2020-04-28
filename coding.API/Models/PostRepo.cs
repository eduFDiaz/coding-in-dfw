using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using coding.API.Dtos;


namespace coding.API.Models
{
    public class PostRepo : IPostRepo
    {
        
        private readonly DataContext _context;
        
        public PostRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<Post> GetPost(int postid)
        {
            if (postid == 0)
                return null;

            var post = await _context.Posts.Include(p => p.PostTags).FirstOrDefaultAsync(p => p.Id == postid);
            var tags = await _context.Tags.Include(t => t.PostTags).ToListAsync();

            return post;
        }
        

        public async Task<bool> AddTagsForPost(PostTag postTag)
        {
            await _context.PostTags.AddAsync(postTag);

            return true;
        }

        public async Task<Post> Create(Post post )
        {
            if (post == null)
                return null;

            await _context.Posts.AddAsync(post);
            
            // user.Posts.AddAsync(post);

            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<List<Post>> GetAllUserPost(int userid)
        {
           var post = await _context.Posts.Where(p => p.UserId == userid).Include(p => p.PostTags).ToListAsync();
           var tags = await _context.Tags.Include(t => t.PostTags).ToListAsync();
                                   
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

        public async Task<bool> EditPost(int postid, Post post)
        {
            var postToEdit = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postid);

            if (postToEdit == null)
                return false;
            
            _context.Posts.Update(postToEdit);

            await _context.SaveChangesAsync();
                        
            return true;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        
    }
}