using AutoMapper;
using TaskMgr.Application.Common.Mappings;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Enums;

namespace TaskMgr.Application.Routines.Queries.GetRoutineDetails;

public class RoutineDetailsDto : IMapWith<Routine>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? DeletedAt { get; set; } = null;
    public DateTime StartTime { get; set; }
    public Status Status { get; set; } = Status.Created;
    public List<Tag>? Tags { get; set; } = new List<Tag>();
    public Target RelatedTarget { get; set; } 
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Routine, RoutineDetailsDto>()
            .ForMember(r => r.Title, opts =>
                opts.MapFrom(r => r.Title))
            .ForMember(r => r.Content, opts =>
                opts.MapFrom(r => r.Content))
            .ForMember(r => r.CreatedAt, opts =>
                opts.MapFrom(r => r.CreatedAt))
            .ForMember(r => r.DeletedAt, opts =>
                opts.MapFrom(r => r.DeletedAt))
            .ForMember(r => r.StartTime, opts =>
                opts.MapFrom(r => r.StartTime))
            .ForMember(r => r.Status, opts =>
                opts.MapFrom(r => r.Status))
            .ForMember(r => r.Tags, opts =>
                opts.MapFrom(r => r.Tags))
            .ForMember(r => r.RelatedTarget, opts =>
                opts.MapFrom(r => r.RelatedTarget));
    }
    
}