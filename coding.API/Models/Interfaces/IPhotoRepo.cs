using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using coding.API.Models;
using coding.API.Models.Entities.Photos;
using coding.API.Models.Entities.Users;


namespace coding.API.Models.Interfaces
{
    public interface IPhotoRepo
    {
        
        Task<User> GetUser(int userid);
        Task<bool> SaveAll();
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
        Task<bool> DeletePhoto(int photoId);
        
       
    }
}