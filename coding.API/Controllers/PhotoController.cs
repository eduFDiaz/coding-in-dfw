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

            // if(!userFromRepo.Photos.Any(u => u.IsMain))
            //     photo.IsMain = true;
      
            userFromRepo.Photos.Add(photo);
                                             
            if (await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                // return CreatedAtRoute("GetPhoto", new { id = photo.PublicId } , photoToReturn );
                return Ok(photoToReturn);
            }

            return BadRequest("Could not add the photo");

        }

    }
}