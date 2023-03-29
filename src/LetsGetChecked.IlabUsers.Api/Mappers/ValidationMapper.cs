using FluentValidation.Results;
using LetsGetChecked.IlabUsers.Api.Contracts.Responses;
using LetsGetChecked.IlabUsers.Api.Validators;

namespace LetsGetChecked.IlabUsers.Api.Mappers;

public static class ValidationMapper
{
    public static ValidationFailureResponse ToResponse(this IEnumerable<ValidationFailure> validationFailures)
    {
        return new ValidationFailureResponse()
        {
            Errors = validationFailures.Select(x => new ValidationResponse
            {
                PropertyName = x.PropertyName,
                Message = x.ErrorMessage
            })
        };
    }
    
    public static ValidationFailureResponse ToResponse(this ValidationFailed failed)
    {
        return new ValidationFailureResponse()
        {
            Errors = failed.Errors.Select(x => new ValidationResponse
            {
                PropertyName = x.PropertyName,
                Message = x.ErrorMessage
            })
        };
    }

    
}

