using AutoMapper;
using TaskMgr.Infrastructure.Identity;
using TaskMgr.WebApi.ViewModels;

namespace TaskMgr.WebApi.Mappings;

public class WebApiMappingProfile : Profile
{
    public WebApiMappingProfile()
    {
        CreateMap<User, RegisterVM>().ReverseMap();
    }
}