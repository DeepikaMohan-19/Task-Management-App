using AutoMapper;
using TMA.Application.DTOs;
using TMA.Domain.Entities;

namespace TMA.Application.MappingProfiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskEntity, TaskDto>();
            CreateMap<TaskDto, TaskEntity>();
        }
    }
}
