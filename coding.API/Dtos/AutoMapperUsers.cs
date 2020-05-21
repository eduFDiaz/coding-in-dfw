using System.Linq;
using AutoMapper;
using coding.API.Models.Posts.Comments;
using coding.API.Models.Products;
using coding.API.Dtos.Products;
using coding.API.Models.Users;
using coding.API.Models.Tags;
using coding.API.Models.Posts;
using coding.API.Models.Photos;
using coding.API.Models.PostTags;
using coding.API.Models.Projects;
using coding.API.Models.Languages;
using coding.API.Models.Educations;
using coding.API.Models.Skills;
using coding.API.Models.Awards;
using coding.API.Models.Interests;
using coding.API.Models.WorkExperiences;
using System.Collections.Generic;
using coding.API.Dtos.Requirements;
using coding.API.Dtos.Comments;
using coding.API.Models.Products.Requirements;
using coding.API.Models.Products.ProductsRequirements;
using coding.API.Dtos.Posts;
using coding.API.Models.Messages;

namespace coding.API.Dtos
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Users Maps
            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserForDetailedDto, User>();
            CreateMap<User, UserForDetailedDto>();
            CreateMap<User, UserForUpdateDto>();
            CreateMap<UserForUpdateDto, User>();

            //Messages Maps
            CreateMap<CreateMessageDto, Message>();
            CreateMap<Message, CreateMessageDto>();
            
            //Posts Maps
            CreateMap<PostForCreateDto, Post>();
            CreateMap<Post, PostForCreateDto>();
            CreateMap<PostForDetailDto, Post>();
            CreateMap<List<Post>, PostForDetailDto>();
            CreateMap<PostForUpdateDto, Post>().ForMember(post => post.Id, y => y.Ignore());
            CreateMap<Post, PostInCommentDetailDto>();
            CreateMap<Post, PostAllCommentDetailDto>();
            // CreateMap<Post, PostForUpdateDto>();

            //Tags Maps
            CreateMap<TagForCreateDto, Tag>();
            CreateMap<TagForDetailDto, Tag>();
            CreateMap<TagForUpdateDto, Tag>();
            CreateMap<Tag, TagForDetailDto>();



            //Products Maps
            CreateMap<ProductForCreateDto, Product>();
            CreateMap<ProductForUpdateDto, Product>();
            CreateMap<Product, ProductForDetailDto>();
            CreateMap<ProductForDetailDto, User>();
            CreateMap<Product, ProductRequirementForDetailDto>();
            CreateMap<ProductRequirement, ProductForDetailDto>();
            CreateMap<ProductRequirement, ProductRequirementForDetailDto>();
            
            //Photo Mapings
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<PhotoForDetailDto, Photo>();
            CreateMap<PhotoForReturnDto, Photo>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<Photo, PhotoForDetailDto>();

            // PostsTags Mappings
            CreateMap<PostTagForCreateDto, PostTag>();
            CreateMap<PostTag, PostTagForDetailDto>();


            // Projects Mappings
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();


            // Education Mappings
            CreateMap<CreateEducationDto, Education>();
            CreateMap<UpdateEducationDto, Education>();

            // Award Mappings
            CreateMap<CreateAwardDto, Award>();
            CreateMap<UpdateAwardDto, Award>();

            // Interest Mappings
            CreateMap<CreateInterestDto, Interest>();
            CreateMap<UpdateInterestDto, Interest>();


            // Skill Mappings
            CreateMap<CreateSkillDto, Skill>();
            CreateMap<UpdateSkillDto, Skill>();

            // WE mappings
            CreateMap<CreateWorkExperienceDto, WorkExperience>();
            CreateMap<UpdateWorkExperienceDto, WorkExperience>();

            // Language Mappings
            CreateMap<UpdateLanguageDto, Language>();
            CreateMap<CreateLanguageDto, Language>();

            // CreateMap<RequirementForCreation, Requirement>();
            // CreateMap<Requirement, RequirementForDetailDto>();
            CreateMap<RequirementForCreationDto, Requirement>();
            CreateMap<Requirement, RequirementForDetailDto>();

            CreateMap<ProductRequirementForCreateDto, ProductRequirement>();
            CreateMap<ProductRequirement, RequirementForDetailDto>();


            CreateMap<CreateCommentDto, Comment>();
            CreateMap<Comment, CommentForDetailDto>();
            CreateMap<Comment, CommentClearForDetailDto>();


        }



    }
}