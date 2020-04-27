using System.Linq;
using AutoMapper;
using coding.API.Models;
using coding.API.Models.Products;
using coding.API.Dtos.Products;


namespace coding.API.Dtos
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserForDetailedDto, User>();
            CreateMap<User, UserForDetailedDto>();
            CreateMap<User, UserForUpdateDto>();
            CreateMap<UserForUpdateDto, User>();

            //Posts Maps
            CreateMap<PostForCreateDto, Post>();
            CreateMap<PostForDetailDto, Post>();
            CreateMap<Post, PostForDetailDto>();
            CreateMap<PostForUpdateDto, Post>().ForMember(post => post.Id, y => y.Ignore());
            // CreateMap<Post, PostForUpdateDto>();

            //Tags Maps
            CreateMap<TagForCreateDto, Tag>();
            CreateMap<TagForDetailDto, Tag>();
            CreateMap<TagForUpdateDto, Tag>();
            CreateMap<Tag, TagForDetailDto>();


            //Products Maps
            CreateMap<ProductForCreateDto, Product>();
            CreateMap<ProductForDetailDto, Product>();
            CreateMap<ProductForUpdateDto, Product>();
            CreateMap<Product, ProductForDetailDto>();
            CreateMap<ProductForDetailDto, User>();

            //Photo Mapings
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<PhotoForDetailDto, Photo>();
            CreateMap<PhotoForReturnDto, Photo>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<Photo, PhotoForDetailDto>();

            // PostsTags Mappings
            CreateMap<PostTagForCreateDto, PostTag>();
            CreateMap<PostTag, PostTagForDetailDto>();
            


        }
        
    }
}