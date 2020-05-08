using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using coding.API.Models.Entities.Users;

namespace coding.API.Models.Interfaces
{
    public interface IRepo
    {
        Task<User> GetUser(int id);
        Task<List<User>> GetUsers();
        Task<bool> SaveAll();
        
    }
}