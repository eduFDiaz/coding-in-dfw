using coding.API.Models;
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

namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController : ControllerBase
    {
    
        private readonly IPhotoRepo _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        
        public PhotoController(IPhotoRepo repo, IConfiguration config, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this._repo = repo;
            this._config = config;
            this._mapper = mapper;
                     
             _cloudinaryConfig = cloudinaryConfig;

            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret 
            );

            _cloudinary = new Cloudinary(account);

        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            // Only if the claim is valid the user is retrieved
            var userFromRepo = await _repo.GetUser(userId);

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
            // photoForCreationDto.UserId = userFromRepo.Id;

            var photo = _mapper.Map<Photo>(photoForCreationDto);

            // var createdPhoto = await _repo.CreatePhoto(photo);

            // if (!createdPhoto)
            //     return BadRequest("Could not add the photo");
            
            // var photoToReturn = _mapper.Map<PhotoForReturnDto>(createdPhoto);

            // return Ok();

            if(!userFromRepo.Photos.Any(u => u.IsMain))
                photo.IsMain = true;
      
            userFromRepo.Photos.Add(photo);
                                             
            if (await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                // return CreatedAtRoute("GetPhoto", new { id = photo.PublicId } , photoToReturn );
                return Ok(photoToReturn);
            }

            return BadRequest("Could not add the photo");

        }

        // Set main photo using id as the photo id
        [HttpPost("{photoId}/setMain")]
        public async Task<IActionResult> SetMain(int userId, int photoId)
        {
            // Checks if the picture belongs to the user
            var user = await _repo.GetUser(userId);
            if(!user.Photos.Any(p => p.Id == photoId))
                return Unauthorized();
             
            // Retrieve photo to be set as main
            var photoFromRepo = await _repo.GetPhoto(photoId);
            if(photoFromRepo.IsMain)
                return BadRequest("This is already the main photo");
            
            // Retrieve current main photo
            var currentMainPhoto = await _repo.GetMainPhotoForUser(userId);
            
            // Set main photo a
            currentMainPhoto.IsMain = false;
            photoFromRepo.IsMain = true;

            if(await _repo.SaveAll())
                return NoContent();

            return BadRequest("Could not set photo to main");
        }

        // Delete photo using id as the photo id
        [HttpDelete("{photoId}")]
        public async Task<IActionResult> DeletePhoto(int userId, int photoId)
        {
            // Checks if the picture belongs to the user
            var user = await _repo.GetUser(userId);
            if(!user.Photos.Any(p => p.Id == photoId))
                return Unauthorized();
             
            // Check if photo is set as main, don't delete if that is true
            var photoFromRepo = await _repo.GetPhoto(photoId);
            if(photoFromRepo.IsMain)
                return BadRequest("Cannot delete the main photo");

            // Let's check if the photo is storaged at Cloudinary so it can also be
            // deleted from cloudinary
            if(photoFromRepo.PublicId != null){
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);
                var result = _cloudinary.Destroy(deleteParams);

                // The Result from Destroying the cloudinary image is Ok
                if(result.Result == "ok") {
                    await _repo.DeletePhoto(photoFromRepo.Id);
                }
            } else {
                // if the photo is not hosted on cloudinary
                await _repo.DeletePhoto(photoFromRepo.Id);
            }
            
            // Saving changes to repo
            if(await _repo.SaveAll())
                return Ok();

            return BadRequest("Could not delete the photo");
        }

    }
}