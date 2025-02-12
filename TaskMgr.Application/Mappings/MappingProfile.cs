using AutoMapper;
using TaskMgr.Application.DTOs;
using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BaseTaskEntity, BaseTaskDTO>().ReverseMap();
    }
}