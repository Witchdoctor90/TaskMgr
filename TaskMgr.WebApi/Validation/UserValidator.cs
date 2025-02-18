using FluentValidation;
using TaskMgr.Infrastructure.Identity;

namespace TaskMgr.WebApi.Validation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.UserName)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 50).WithMessage("Name must be between 2 and 50 characters");
    }
}