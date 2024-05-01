namespace TaskMgr.Application.Targets.Queries.GetAll;

public class TargetsListVm
{
    public IList<TargetLookupDto> Targets { get; set; } = new List<TargetLookupDto>();
}