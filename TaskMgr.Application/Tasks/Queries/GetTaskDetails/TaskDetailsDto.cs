using AutoMapper;
using TaskMgr.Application.Common.Mappings;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Enums;

namespace TaskMgr.Application.Tasks.Queries.GetTaskDetails;

public class TaskDetailsDto : IMapWith<TaskEntity>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime StartTime { get; set; }
    public Status Status { get; set; }
    public Target RelatedTarget { get; set; }
    public List<Tag> Tags { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TaskDetailsDto, TaskEntity>()
            .ForMember(t => t.Title, opts =>
                opts.MapFrom(t => t.Title))
            .ForMember(t => t.Content, opts =>
                opts.MapFrom(t => t.Content))
            .ForMember(t => t.CreatedAt, opts =>
                opts.MapFrom(t => t.CreatedAt))
            .ForMember(t => t.DeletedAt, opts =>
                opts.MapFrom(t => t.DeletedAt))
            .ForMember(t => t.StartTime, opts =>
                opts.MapFrom(t => t.DeletedAt))
            .ForMember(t => t.StartTime, opts =>
                opts.MapFrom(t => t.StartTime))
            .ForMember(t => t.Status, opts =>
                opts.MapFrom(t => t.Status))
            .ForMember(t => t.RelatedTarget, opts =>
                opts.MapFrom(t => t.RelatedTarget))
            .ForMember(t => t.Tags, opts =>
                opts.MapFrom(t => t.Tags));
    }
}