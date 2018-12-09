using System;
using AutoMapper;
using Entity = Bookstore.Entities.Implementations;
using ResponseDto = Bookstore.Model.ResponseDto;

namespace Bookstore.Profiles
{
    public class EntityToResponseMappingProfile : Profile
    {
        public EntityToResponseMappingProfile()
        {
            CreateMap<Entity.Category, ResponseDto.Category>();
            CreateMap<Entity.Author, ResponseDto.Author>();
            CreateMap<Entity.Book, ResponseDto.Book>();
        }
    }
}
