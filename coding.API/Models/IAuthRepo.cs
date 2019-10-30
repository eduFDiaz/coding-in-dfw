using System.Threading.Tasks;

namespace coding.API.Models
{
    public interface IAuthRepo
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string email, string password);
         Task<bool> UserExists(string email);
    }
}