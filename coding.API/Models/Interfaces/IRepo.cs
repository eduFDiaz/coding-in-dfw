using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace coding.API.Models
{
    public interface IRepo
    {
        Task<User> GetUser(int id);
        Task<List<User>> GetUsers();
        Task<bool> SaveAll();
        
    }
}