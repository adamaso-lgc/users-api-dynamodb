using LetsGetChecked.IlabUsers.Api.Contracts.Requests;
using LetsGetChecked.IlabUsers.Api.Contracts.Responses;
using LetsGetChecked.IlabUsers.Api.Validators;
using OneOf;
using OneOf.Types;

namespace LetsGetChecked.IlabUsers.Api.Services;

public interface IUserService
{
    Task<OneOf<UserResponse, ValidationFailed>> CreateAsync(CreateUserRequest request);

    Task<OneOf<UserResponse, NotFound>> GetAsync(Guid id);

    Task<OneOf<UserResponse, NotFound, ValidationFailed>> UpdateAsync(Guid id, UpdateUserRequest request);

    Task<OneOf<Success, NotFound>> DeleteAsync(Guid id);
}