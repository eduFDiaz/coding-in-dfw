using System.Threading.Tasks;
using coding.API.Models.Entities.Users;

namespace coding.API.Models.Interfaces
{
    public interface IAuthRepo
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string email);
    }
}