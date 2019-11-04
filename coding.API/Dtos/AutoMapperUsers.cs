using System.Linq;
using AutoMapper;
using coding.API.Models;

namespace coding.API.Dtos
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserForDetailedDto, User>();
            CreateMap<User, UserForDetailedDto>();
        }
        
    }
}