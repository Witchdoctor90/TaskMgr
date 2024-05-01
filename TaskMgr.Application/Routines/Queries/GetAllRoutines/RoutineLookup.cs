using AutoMapper;
using TaskMgr.Application.Common.Mappings;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Enums;

namespace TaskMgr.Application.Routines.Queries.GetAllRoutines;

public class RoutineLookup : IMapWith<Routine>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Status Status { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Routine, RoutineLookup>()
            .ForMember(r => r.Title, opts =>
                opts.MapFrom(r => r.Title))
            .ForMember(r => r.Content, opts =>
                opts.MapFrom(r => r.Content))
            .ForMember(r => r.Status, opts =>
                opts.MapFrom(r => r.Status));
    }
}