using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Targets.Queries.GetTargetDetails;

public class GetTargetDetailsQueryHandler : IRequestHandler<GetTargetDetailsQuery, TargetDetailsDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTargetDetailsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TargetDetailsDto> Handle(GetTargetDetailsQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Targets.FirstOrDefaultAsync(t => t.Id == request.Id);
        if (result is null) throw new ItemNotFoundException(request.Id);
        
        return _mapper.Map<TargetDetailsDto>(result);
    }
}