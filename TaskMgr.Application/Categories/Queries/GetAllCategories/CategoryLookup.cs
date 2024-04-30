using AutoMapper;
using TaskMgr.Application.Common.Mappings;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Categories.Queries.GetAllCategories;

public class CategoryLookup : IMapWith<Routine>
{
    public string Title { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, CategoryLookup>()
            .ForMember(c => c.Title, opts =>
                opts.MapFrom(c => c.Title));
    }
}