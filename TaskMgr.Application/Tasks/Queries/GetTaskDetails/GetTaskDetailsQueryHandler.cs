using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Tasks.Queries.GetTaskDetails;

public class GetTaskDetailsQueryHandler : IRequestHandler<GetTaskDetailsQuery, TaskDetailsDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTaskDetailsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<TaskDetailsDto> Handle(GetTaskDetailsQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id);
        if (result is null) throw new ItemNotFoundException(request.Id);

        return _mapper.Map<TaskDetailsDto>(result);
    }
}