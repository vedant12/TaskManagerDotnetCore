using AutoMapper;
using TaskManager.Core.Dtos;
using TaskManager.Core.Entities;

namespace TaskManager.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskItem, TaskCreateDto>()
                .ReverseMap();
            CreateMap<TaskItem, TaskReadDto>()
                .ReverseMap();
        }
    }
}