using AutoMapper;
using RestaurantMVC.Entities;
using RestaurantMVC.Models;

namespace RestaurantMVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
