using AutoMapper;
using TaskMgr.Application.Common.Mappings;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Enums;

namespace TaskMgr.Application.Targets.Queries.GetAll;

public class TargetLookupDto : IMapWith<Target>
{
    public string Title { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Target, TargetLookupDto>()
            .ForMember(t => t.Title, opt =>
                opt.MapFrom(t => t.Title))
            .ForMember(t => t.Status, opt => opt.MapFrom(t =>
                t.Status))
            .ForMember(t => t.CreatedAt, opt =>
                opt.MapFrom(t => t.CreatedAt));
    }
}