using System.Linq;
using AutoMapper;
using coding.API.Models.Entities;
using coding.API.Models.Entities.Products;
using coding.API.Models.Entities.Users;
using coding.API.Models.Entities.Photos;
using coding.API.Models.Entities.Awards;
using coding.API.Models.Entities.Skills;
using coding.API.Models.Entities.Projects;
using coding.API.Models.Entities.WorkExperiences;
using coding.API.Models.Entities.Educations;
using coding.API.Models.Entities.Tags;
using coding.API.Models.Entities.Posts;
using coding.API.Models.Entities.PostTags;
using coding.API.Models.Entities.Languages;
using coding.API.Models.Entities.Products.Requirements;
using coding.API.Models.Entities.Products.ProductsRequirements;



using coding.API.Dtos.Products;
using coding.API.Dtos.Requirements;

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


            // ProductRequirment Mappings
            CreateMap<ProductRequirementForCreateDto, ProductRequirement>();
            

             // Projects Mappings
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();        


            // Education Mappings
            CreateMap<CreateEducationDto, Education>();
            CreateMap<UpdateEducationDto, Education>();        

            // Award Mappings
            CreateMap<CreateAwardDto, Award>();
            CreateMap<UpdateAwardDto, Award>(); 
            

            // Skill Mappings
            CreateMap<CreateSkillDto, Skill>();
            CreateMap<UpdateSkillDto, Skill>(); 
            
            // WE mappings
            CreateMap<CreateWorkExperienceDto, WorkExperience>();
            CreateMap<UpdateWorkExperienceDto, WorkExperience>();

            // Language Mappings
            CreateMap<UpdateLanguageDto, Language>();
            CreateMap<CreateLanguageDto, Language>();

            CreateMap<RequirementForCreationDto, Requirement>();
            CreateMap<Requirement, RequirementForDetailDto>();
        }
    
    }
}