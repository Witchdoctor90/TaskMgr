using AutoMapper;
using TaskMgr.Application.Common.Mappings;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Tags.Queries.GetAllTags;

public class TagLookup : IMapWith<Tag>
{
    public string Title { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Tag, TagLookup>()
            .ForMember(t => t.Title, opts =>
                opts.MapFrom(t => t.Title));
    }
}