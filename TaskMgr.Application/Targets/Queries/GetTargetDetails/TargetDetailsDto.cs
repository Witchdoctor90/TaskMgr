using AutoMapper;
using TaskMgr.Application.Common.Mappings;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Enums;

namespace TaskMgr.Application.Targets.Queries.GetTargetDetails;

public class TargetDetailsDto : IMapWith<Target>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? DeletedAt { get; set; }
    public DateTime StartTime { get; set; }
    public Status Status { get; set; } = Status.Created;
    public List<Tag> Tags { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Target, TargetDetailsDto>()
            .ForMember(t => t.Title, opts =>
                opts.MapFrom(t => t.Title))
            .ForMember(t => t.Content, opts =>
                opts.MapFrom(t => t.Content))
            .ForMember(t => CreatedAt, opts =>
                opts.MapFrom(t => t.CreatedAt))
            .ForMember(t => t.DeletedAt, opts =>
                opts.MapFrom(t => t.DeletedAt))
            .ForMember(t => t.StartTime, opts =>
                opts.MapFrom(t => t.StartTime))
            .ForMember(t => t.Status, opts =>
                opts.MapFrom(t => t.Status))
            .ForMember(t => t.Tags, opts =>
                opts.MapFrom(t => t.Tags))
            .ForMember(t => t.Categories, opts =>
                opts.MapFrom(t => t.Categories));
    }
}