using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace coding.API.Models
{
    public class Repo : IRepo
    {
        public DataContext _context;
        
        public Repo(DataContext context)
        {
            this._context = context;

        }
        public async Task<List<User>> GetUsers()
        {
            var users = await this._context.Users.ToListAsync();
            /* users = new List<User>{
                new User{
                    Username="Eduardo",
                    Email="fernandez9000@gmail.com"
                },
                new User{
                    Username="Eduardo2",
                    Email="2fernandez9000@gmail.com"
                }
            }; */
            return users;
        }
    }
}