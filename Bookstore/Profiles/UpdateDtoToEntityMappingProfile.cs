using System;
using AutoMapper;
using UpdateDto = Bookstore.Model.UpdateDto;
using Entity = Bookstore.Entities.Implementations;

namespace Bookstore.Profiles
{
    public class UpdateDtoToEntityMappingProfile : Profile
    {
        public UpdateDtoToEntityMappingProfile()
        {
            CreateMap<UpdateDto.Category, Entity.Category>();
            CreateMap<UpdateDto.Author, Entity.Author>();
            CreateMap<UpdateDto.Book, Entity.Book>();
        }
    }
}
