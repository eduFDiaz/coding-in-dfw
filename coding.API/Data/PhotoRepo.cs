using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Extensions.Options;
using coding.API.Models.Entities.Photos;
using coding.API.Models.Entities.Users;

using coding.API.Models.Interfaces;

namespace coding.API.Data
{
    public class PhotoRepo : IPhotoRepo
    {
        
        private readonly DataContext _context;
        
        public PhotoRepo(DataContext context)
        {
            _context = context;
           
        }

      public async Task<bool> DeletePhoto(int photoId)
      {
          var photoToDelte = await _context.Photos.FirstOrDefaultAsync(p => p.Id == photoId);

          if (photoToDelte == null)
                return false;

            _context.Photos.Remove(photoToDelte);
            
            // await _context.SaveChangesAsync();

            return true;
      }

        public async Task<User> GetUser(int userid)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == userid);
            return user;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(x => x.Id == id);
            return photo;
        }

        public async Task<bool> SaveAll()
        {
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            var photo = await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
            return photo;
        }

        // public async Task<bool> CreatePhoto(Photo photo)
        // {
        //     var createdPhoto = await _context.Photos.AddAsync(photo);
            
        //     await _context.SaveChangesAsync();

        //     return true;
        // }
    
    }
}