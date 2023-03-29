using FluentValidation;
using LetsGetChecked.IlabUsers.Api.Domain.Entities;

namespace LetsGetChecked.IlabUsers.Api.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}