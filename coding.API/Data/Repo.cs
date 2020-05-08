using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using coding.API.Models.Entities.Users;
using coding.API.Models.Entities.Photos;


using coding.API.Models.Interfaces;


namespace coding.API.Data
{
    public class Repo : IRepo
    {
        public DataContext _context;
            
        public Repo(DataContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await this._context.Users.FirstOrDefaultAsync(user => user.Id == id);

            var posts = await this._context.Posts.Where(post => post.UserId == id).ToListAsync();

            var products = await this._context.Products.Where(product => product.UserId == id).ToListAsync();

            var photos = await this._context.Photos.Where(photo => photo.UserId == id).ToListAsync();
            
            return user;
        
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await this._context.Users.Include(p => p.Posts).Include(prod => prod.Products).Include(p => p.Photos).ToListAsync();
            return users;
            // return this.users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}