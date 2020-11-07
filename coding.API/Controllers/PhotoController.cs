using System;
using System.Threading.Tasks;
using AutoMapper;
using coding.API.Dtos;
using Microsoft.AspNetCore.Mvc;
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
using coding.API.Models.Products;
using Microsoft.AspNetCore.Authorization;
using coding.API.Dtos.Products;
using coding.API.Models.Posts;
using coding.API.Dtos.Posts;


namespace coding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController : ControllerBase
    {

        private readonly IRepository<Photo> _photoDal;
        private readonly IRepository<User> _userDal;
        private readonly IRepository<Product> _productDal;

        private readonly IRepository<ProductPhoto> _productPhotoDal;

        private readonly IRepository<Post> _postDal;

        private readonly IRepository<PostPhoto> _postPhotoDal;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;



        public PhotoController(IRepository<Post> postDal, IRepository<PostPhoto> postPhotoDal, IRepository<ProductPhoto> productPhotoDal, IRepository<Photo> photoDal, IRepository<User> userDal, IRepository<Product> productDal, IConfiguration config, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _photoDal = photoDal;
            _userDal = userDal;

            _productDal = productDal;
            _productPhotoDal = productPhotoDal;

            _postDal = postDal;
            _postPhotoDal = postPhotoDal;

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

        [Authorize]
        [HttpPost("{userId}/create")]
        public async Task<IActionResult> AddPhotoForUser(Guid userId, [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            var userFromRepo = (await _userDal.ListAsync())
                    .SingleOrDefault(u => u.Id == userId);

            var file = photoForCreationDto.File;

            var uploadResults = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
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
            var lolo = (await _photoDal.ListAsync())
                .Where(p => p.UserId == userFromRepo.Id)
                .FirstOrDefault(p => p.IsMain);

            photo.IsMain = lolo == default;


            await _photoDal.Add(photo);

            return Ok(new PhotoPresenter(photo));

        }

        [Authorize]
        [HttpPost("{itemId}/{photoId}/setMain")]
        public async Task<IActionResult> SetMain(Guid itemId, string type, Guid photoId)
        {
            switch (type)
            {
                case "product":

                    var ProductPhotoFromRepo = await _productPhotoDal.GetById(photoId);
                    if (ProductPhotoFromRepo.IsMain)
                        return BadRequest("This is already the main photo");
                    var currentMainPhoto = _productPhotoDal.ListAll().Where(p => p.ProductId == itemId).FirstOrDefault(p => p.IsMain);

                    // Set main photo 
                    currentMainPhoto.IsMain = false;
                    ProductPhotoFromRepo.IsMain = true;
                    if (await _productPhotoDal.SaveAll())
                        return NoContent();

                    return BadRequest("Could not set photo to main");

                case "post":

                    var PostPhotoFromRepo = await _postPhotoDal.GetById(photoId);
                    if (PostPhotoFromRepo.IsMain)
                        return BadRequest("This is already the main photo");
                    var currentMainPostPhoto = _postPhotoDal.ListAll().Where(p => p.PostId == itemId).FirstOrDefault(p => p.IsMain);

                    // Set main photo 
                    currentMainPostPhoto.IsMain = false;
                    PostPhotoFromRepo.IsMain = true;
                    if (await _postPhotoDal.SaveAll())
                        return NoContent();

                    return BadRequest("Could not set photo to main");



                default:
                    var photoFromRepo = await _photoDal.GetById(photoId);
                    if (photoFromRepo.IsMain)
                        return BadRequest("This is already the main photo");
                    var currentMainUserPhoto = _photoDal.ListAll().Where(p => p.UserId == itemId).FirstOrDefault(p => p.IsMain);

                    // Set main photo 
                    currentMainUserPhoto.IsMain = false;
                    photoFromRepo.IsMain = true;
                    if (await _photoDal.SaveAll())
                        return NoContent();

                    return BadRequest("Could not set photo to main");


            }




        }

        // Create photo for product
        [Authorize]
        [HttpPost("product/{productId}/create")]
        public async Task<IActionResult> AddPhotoForProduct(Guid productId, [FromForm] ProductPhotoForCreationDto photoForCreationDto)
        {
            
            var productFromRepo = (await _productDal.ListAsync())
                    .FirstOrDefault(p => p.Id == productId);

            var file = photoForCreationDto.File;

            var uploadResults = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),

                    };
                    uploadResults = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResults.Uri.ToString();
            photoForCreationDto.PublicId = uploadResults.PublicId;
            photoForCreationDto.ProductId = productFromRepo.Id;

            var photo = _mapper.Map<ProductPhoto>(photoForCreationDto);

            // simplify expresion
            var productPhoto = (await _productPhotoDal.ListAsync())
                .Where(p => p.ProductId == productFromRepo.Id)
                .FirstOrDefault(p => p.IsMain);

            photo.IsMain = productPhoto == default;


            await _productPhotoDal.Add(photo);

            return Ok(new ProductPhotoPresenter(photo));

        }
        [Authorize]
        [HttpPost("post/{postId}/create")]
        public async Task<IActionResult> AddPhotoForPost(Guid postId, [FromForm] PostPhotoForCreationDto photoForCreationDto)
        {
            // Only if the claim is valid the user is retrieved
            var postFromRepo = (await _postDal.ListAsync())
                    .FirstOrDefault(p => p.Id == postId);

            var file = photoForCreationDto.File;

            var uploadResults = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };
                    uploadResults = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResults.Uri.ToString();
            photoForCreationDto.PublicId = uploadResults.PublicId;
            photoForCreationDto.PostId = postFromRepo.Id;

            var photo = _mapper.Map<PostPhoto>(photoForCreationDto);

            // simplify expresion
            var postPhoto = (await _postPhotoDal.ListAsync())
                .Where(p => p.PostId == postFromRepo.Id)
                .FirstOrDefault(p => p.IsMain);

            photo.IsMain = postPhoto == default;


            await _postPhotoDal.Add(photo);

            return Ok(new PostPhotoPresenter(photo));

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

            if (photoFromRepo.IsMain)
                return BadRequest("Cannot delete the main photo");

            // Let's check if the photo is storaged at Cloudinary so it can also be
            // deleted from cloudinary
            if (photoFromRepo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);
                var result = _cloudinary.Destroy(deleteParams);

                // The Result from Destroying the cloudinary image is Ok
                if (result.Result == "ok")
                {
                    if (await _photoDal.Delete(photoFromRepo))
                        return NoContent();
                }

            }
            else
            {
                // if the photo is not hosted on cloudinary
                if (await _photoDal.Delete(photoFromRepo))
                    return NoContent();
            }
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
