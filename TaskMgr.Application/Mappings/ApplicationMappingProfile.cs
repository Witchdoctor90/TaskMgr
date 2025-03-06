using AutoMapper;
using TaskMgr.Application.DTOs;
using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Application.Mappings;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<BaseTaskEntity, BaseTaskDTO>().ReverseMap();
    }
}