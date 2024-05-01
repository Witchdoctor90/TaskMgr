using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;

namespace TaskMgr.Application.Routines.Queries.GetAllRoutines;

public class GetAllRoutinesQueryHandler : IRequestHandler<GetAllRoutinesQuery, RoutinesListVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllRoutinesQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RoutinesListVm> Handle(GetAllRoutinesQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Routines.ProjectTo<RoutineLookup>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return new RoutinesListVm() { Routines = list };
    }
}