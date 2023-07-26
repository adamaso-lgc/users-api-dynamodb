using LetsGetChecked.IlabUsers.Api.Contracts.Requests;
using LetsGetChecked.IlabUsers.Api.Contracts.Responses;
using LetsGetChecked.IlabUsers.Api.Domain.Entities;

namespace LetsGetChecked.IlabUsers.Api.Mappers;

public static class UserMapper
{
    public static User ToUser(this CreateUserRequest createUserRequest)
    {
        return new User()
        {
            Name = createUserRequest.Name,
            Email = createUserRequest.Email,
            Password = createUserRequest.Password,
            Role = createUserRequest.Role
        };
    }
    
    public static User ToUser(this UpdateUserRequest createUserRequest, Guid id)
    {
        return new User()
        {
            Id = id,
            Name = createUserRequest.Name,
            Email = createUserRequest.Email,
            Role = createUserRequest.Role
        };
    }
    
    public static UserResponse ToUserResponse(this User user)
    {
        return new UserResponse()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            CreatedDate = user.CreatedDate,
            LastModifiedDate = user.LastModifiedDate
        };
    }
}