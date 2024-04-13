using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;

namespace TaskMgr.Application.Targets.Queries.GetAll;

public class GetAllTargetsQueryHandler : IRequestHandler<GetAllTargetsQuery, TargetsListVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllTargetsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TargetsListVm> Handle(GetAllTargetsQuery request, CancellationToken cancellationToken)
    {
        var targetsQuery =
            await _context.Targets.ProjectTo<TargetLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        return new TargetsListVm() { Targets = targetsQuery };
    }
}