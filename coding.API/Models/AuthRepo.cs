using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace coding.API.Models
{
    public class AuthRepo : IAuthRepo
    {
        
        private readonly DataContext _context;
        public AuthRepo(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string email, string password)
        {
            // Here we allow the user to use its username or its email to login
            var user = await _context.Users.Include(p => p.Photos).Include(b => b.Posts)
                                        .FirstOrDefaultAsync(x => x.Email == email || x.Username == email);
            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            
            // The hash and the salt are passed as reference instead of as values
            this.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {

            // The class HMACSHA512 implements dispose so we can get rid of the var as soon as we
            // finished using it, that will make this part of the api more secure to attacks
            // https://stackoverflow.com/questions/18080445/difference-between-hmacsha256-and-hmacsha512
            // URL above learn why hmacsha512 is safe, performant and compatible
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                // passwordSalt is the key to verify the password when the user logs
                passwordSalt = hmac.Key;
                // password hash is just the computed hash of the password using the HMACSHA512 algorithm
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string email)
        {
            // Method to check if the user exists
            if (await _context.Users.AnyAsync(x => x.Username == email && x.Email == email))
                return true;

            return false;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // Compute the password hash using the passwordSalt stored in the database
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                // Verify that the password computedHash is equal to the stored passwordHash
                for (int i = 0; i < computedHash.Length; i++)
                    if (passwordHash[i] != computedHash[i])
                        return false;
            }
            return true;
        }
    }
}