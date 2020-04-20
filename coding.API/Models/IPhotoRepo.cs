using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using coding.API.Models;


namespace coding.API.Models
{
    public interface IPhotoRepo
    {
        // Task<Photo> Create(Photo photo);
        Task<User> GetUser(int userid);
        Task<bool> SaveAll();
        // Task<bool> CreatePhoto(Photo photo);
       
    }
}