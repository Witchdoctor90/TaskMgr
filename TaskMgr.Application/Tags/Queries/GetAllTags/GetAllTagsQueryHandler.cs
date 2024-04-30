using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Tags.Queries.GetAllTags;

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, TagsListVm>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetAllTagsQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<TagsListVm> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Tags.ProjectTo<TagLookup>(_mapper.ConfigurationProvider)
            .ToListAsync();
        if (list is null) throw new NotFoundException();
        var result = new TagsListVm()
        {
            Tags = list
        };
        return result;
    }
}