using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Routines.Queries.GetRoutineDetails;

public class GetroutineDetailsQueryHandler : IRequestHandler<GetRoutineDetailsQuery, RoutineDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetroutineDetailsQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<RoutineDetailsDto> Handle(GetRoutineDetailsQuery request, CancellationToken cancellationToken)
    {
        var obj = await _context.Routines.FirstOrDefaultAsync(r => r.Id == request.Id);
        
        if (obj is null)
        {
            throw new ItemNotFoundException(request.Id);
        }
        
        return _mapper.Map<RoutineDetailsDto>(obj);
    }
}