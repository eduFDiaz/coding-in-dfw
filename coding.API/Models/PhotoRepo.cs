using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Extensions.Options;

namespace coding.API.Models
{
    public class PhotoRepo : IPhotoRepo
    {
        
        private readonly DataContext _context;
        
        public PhotoRepo(DataContext context)
        {
            _context = context;
           
        }

        public async Task<User> GetUser(int userid)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == userid);
            return user;
        }

        public async Task<bool> SaveAll()
        {
           return await _context.SaveChangesAsync() > 0;
        }

        // public async Task<bool> CreatePhoto(Photo photo)
        // {
        //     var createdPhoto = await _context.Photos.AddAsync(photo);
            
        //     await _context.SaveChangesAsync();

        //     return true;
        // }
    
    }
}