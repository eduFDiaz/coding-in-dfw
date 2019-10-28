using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace coding.API.Models
{
    public interface IRepo
    {
        Task<List<User>> GetUsers();
    }
}