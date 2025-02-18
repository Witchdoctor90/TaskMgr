using FluentValidation;
using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.WebApi.Validation;

public class BaseTaskEntityValidator<T> : AbstractValidator<T> where T : BaseTaskEntity
{
    public BaseTaskEntityValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(1000).WithMessage("Content cannot exceed 1000 characters.");

        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty).WithMessage("UserId is required.");

        RuleFor(x => x.CreatedAt)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("CreatedAt cannot be in the future.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid TaskStatus.");
    }
}