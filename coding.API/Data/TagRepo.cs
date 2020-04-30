using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace coding.API.Models
{
    public class TagRepo : ITagRepo
    {
        
        private readonly DataContext _context;
        public TagRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<Tag> GetTag(int tagid)
        {
            if (tagid == 0)
                return null;

            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == tagid);

            return tag;
        }

        public async Task<Tag> Create(Tag tag)
        {
            if (tag == null)
                return null;
              

            await _context.Tags.AddAsync(tag);
            
            await _context.SaveChangesAsync();

            return tag;
        }

        // public async Task<List<Tag>> GetAllPostTags(int postid)
        // {
        //    var tag = await _context.Tags.Where(t => t.PostId == postid).ToListAsync();
            
        //    return tag;
        // }

        public async Task<bool> DeleteTag(int tagid)
        {
            var tagToDelete = await _context.Tags.FirstOrDefaultAsync(t => t.Id == tagid);

            if (tagToDelete == null)
                return false;

            _context.Tags.Remove(tagToDelete);
            
            await _context.SaveChangesAsync();

            return true;
        }                

        public async Task<bool> EditTag(int tagid, Tag tag)
        {
            var tagToEdit = await _context.Tags.FirstOrDefaultAsync(t => t.Id == tagid);

            if (tagToEdit == null)
                return false;
            
            _context.Tags.Update(tagToEdit);

            await _context.SaveChangesAsync();
                        
            return true;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Tag>> GetAll()
        {
            var alltags = await _context.Tags.ToListAsync();

            return alltags;
        }
    }
}