using FluentValidation;
using TaskMgr.Domain.Entities;

namespace TaskMgr.WebApi.Validation;

public class TaskEntityValidator : AbstractValidator<TaskEntity>
{
    public TaskEntityValidator()
    {
        Include(new BaseTaskEntityValidator<TaskEntity>());

        RuleFor(x => x.RemindEvery)
            .Must(remindEvery => remindEvery == null || remindEvery > TimeSpan.Zero)
            .WithMessage("RemindEvery must be positive if provided.");

        RuleFor(x => x.Deadline)
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Deadline must be today or in the future.");
    }
}