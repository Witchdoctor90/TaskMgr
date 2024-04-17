using AutoMapper;
using TaskMgr.Application.Common.Mappings;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Enums;

namespace TaskMgr.Application.Tasks.Queries.GetAllTasks;

public class TaskLookupDto : IMapWith<TaskEntity>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Status Status { get; set; }
    public List<Tag> Tags { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TaskEntity, TaskLookupDto>()
            .ForMember(t => t.Title, opts =>
                opts.MapFrom(t => t.Title))
            .ForMember(t => t.Content, opts =>
                opts.MapFrom(t => t.Content))
            .ForMember(t => t.Status, opts =>
                opts.MapFrom(t => t.Status))
            .ForMember(t => t.Tags, opts =>
                opts.MapFrom(t => t.Tags));
    }
}