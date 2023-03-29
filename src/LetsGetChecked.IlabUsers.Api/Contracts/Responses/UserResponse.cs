using LetsGetChecked.IlabUsers.Api.Domain.Enums;

namespace LetsGetChecked.IlabUsers.Api.Contracts.Responses;

public class UserResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public string Email { get; init; } = default!;
    public Roles Role { get; init; }
    public DateTime CreatedDate { get; init; } = default;
    public DateTime? LastModifiedDate { get; init; } = default;
}