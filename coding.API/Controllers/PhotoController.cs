using System;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Linq;
using coding.API.Helpers;
using Microsoft.Extensions.Options;
using coding.API.Data;
using coding.API.Models.Users;
using coding.API.Models.Photos;
using coding.API.Models.Presenter;
using Microsoft.AspNetCore.Authorization;


namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController : ControllerBase
    {
    
        private readonly Repository<Photo> _photoDal;
        private readonly Repository<User> _userDal;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        
                
        public PhotoController(Repository<Photo> photoDal,Repository<User> userDal, IConfiguration config, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _photoDal = photoDal;
            _userDal = userDal;
            _config = config;
            _mapper = mapper;
                     
             _cloudinaryConfig = cloudinaryConfig;

            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret 
            );

            _cloudinary = new Cloudinary(account);

        }

        
        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(Guid userId, [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            // Only if the claim is valid the user is retrieved
            var userFromRepo = (await _userDal.ListAsync())
                    .SingleOrDefault(u => u.Id == userId);

            var file = photoForCreationDto.File;

            var uploadResults = new ImageUploadResult();

            if(file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams(){
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            // .Width("500").Height("500").Crop("fill").Gravity("face")
                            .Width("500").Height("500").Crop("fill")
                    };
                    uploadResults = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResults.Uri.ToString();
            photoForCreationDto.PublicId = uploadResults.PublicId;
            photoForCreationDto.UserId = userFromRepo.Id;

            var photo = _mapper.Map<Photo>(photoForCreationDto);

            // simplify expresion
            var lolo =  (await _photoDal.ListAsync())
                .Where(p => p.UserId == userFromRepo.Id)
                .FirstOrDefault(p => p.IsMain);

            photo.IsMain = lolo == default;

      
            await _photoDal.Add(photo); 
            
            return Ok(new PhotoPresenter(photo));
                               

            // return BadRequest("Could not add the photo");

        }

        // Set main photo using id as the photo id
        [Authorize]
        [HttpPost("{photoId}/setMain")]
        public async Task<IActionResult> SetMain(Guid userId, Guid photoId)
        {
            // // // Checks if the picture belongs to the user
            var userPhotos = (await _photoDal.ListAsync()).Where(p => p.UserId == userId).ToList();

            foreach (var photo in userPhotos)
            {
                if (photo.UserId != userId)
                    return Unauthorized();
            }

            // Retrieve photo to be set as main
             var photoFromRepo = (await _photoDal.GetById(photoId));
            
            if (photoFromRepo.IsMain)
                return BadRequest("This is already the main photo");
            
            // Retrieve current main photo
            var currentMainPhoto = (await _photoDal.ListAsync()).Where(p => p.IsMain).SingleOrDefault();
            
            // Set main photo a
             currentMainPhoto.IsMain = false;
             photoFromRepo.IsMain = true;

            if (await _photoDal.Update(photoFromRepo))
                return NoContent();

            return BadRequest();
           
        }

        // Delete photo using id as the photo id
        [Authorize]
        [HttpDelete("{photoId}")]
        public async Task<IActionResult> DeletePhoto(Guid userId, Guid photoId)
        {
            // // Checks if the picture belongs to the user
            var userPhotos = (await _photoDal.ListAsync()).Where(p => p.UserId == userId).ToList();

            foreach (var photo in userPhotos)
            {
                if (photo.UserId != userId)
                    return Unauthorized();
            }
             
            // Check if photo is set as main, don't delete if that is true
            var photoFromRepo = (await _photoDal.GetById(photoId));
            
            if(photoFromRepo.IsMain)
                return BadRequest("Cannot delete the main photo");

            // Let's check if the photo is storaged at Cloudinary so it can also be
            // deleted from cloudinary
            if (photoFromRepo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);
                var result = _cloudinary.Destroy(deleteParams);

                // The Result from Destroying the cloudinary image is Ok
                if(result.Result == "ok") {
                    await _photoDal.Delete(photoFromRepo);
                }
            
            } else {
                 // if the photo is not hosted on cloudinary
                 await _photoDal.Delete(photoFromRepo);
            }
            
            // Saving changes to repo
            if(await _photoDal.SaveAll())
                return Ok();

            return BadRequest("Could not delete the photo");
        }

        
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPhotos()
        {
            var allphotos = (await _photoDal.ListAsync());

            return Ok(allphotos);
        }

    }
}