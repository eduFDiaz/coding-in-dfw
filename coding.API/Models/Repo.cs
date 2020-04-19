using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;



namespace coding.API.Models
{
    public class Repo : IRepo
    {
        public DataContext _context;
        // private readonly List<User> users = new List<User>{
        //         new User{
        //             Username="Eduardo",
        //             FullName="Eduardo Fernandez",
        //             Email="fernandez9000@gmail.com"
        //         },
        //         new User{
        //             Username="Eduardo2",
        //             FullName="Eduardo Fernaneeedez",
        //             Email="2fernandez9000@gmail.com"
        //         }
        //     };
        
        public Repo(DataContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await this._context.Users.FirstOrDefaultAsync(user => user.Id == id);

            var posts = await this._context.Posts.Where(post => post.UserId == id).ToListAsync();
            
            return user;
        
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await this._context.Users.ToListAsync();
            return users;
            // return this.users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}