using System;
using AutoMapper;
using CreateDto = Bookstore.Model.CreateDto;
using Entity = Bookstore.Entities.Implementations;

namespace Bookstore.Profiles
{
    public class CreateDtoToEntityMappingProfile : Profile
    {
        public CreateDtoToEntityMappingProfile()
        {
            CreateMap<CreateDto.Category, Entity.Category>();
            CreateMap<CreateDto.Author, Entity.Author>();
            CreateMap<CreateDto.Book, Entity.Book>();
            CreateMap<CreateDto.User, Entity.ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
