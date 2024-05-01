using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Categories.Queries.GetAllCategories;

public class GetAllRoutinesQueryHandler : IRequestHandler<GetAllCategoriesQuery, CategoriesListVm>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetAllRoutinesQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<CategoriesListVm> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Categories.ProjectTo<CategoryLookup>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        if (list is null) throw new NotFoundException();
        
        var result = new CategoriesListVm()
        {
            Categories = list
        };
        return result;
    }
}