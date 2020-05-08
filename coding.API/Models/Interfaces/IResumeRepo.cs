using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace coding.API.Models.Interfaces
{
    public interface IResumeRepo
    {
        Task<bool> SaveAll();
        
    }
}