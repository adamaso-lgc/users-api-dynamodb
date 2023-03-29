using FluentValidation.Results;

namespace LetsGetChecked.IlabUsers.Api.Validators;

public record ValidationFailed(IEnumerable<ValidationFailure> Errors)
{
    public ValidationFailed(ValidationFailure error) : this(new[] { error })
    {
    }
}