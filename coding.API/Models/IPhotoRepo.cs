using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using coding.API.Models;


namespace coding.API.Models
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