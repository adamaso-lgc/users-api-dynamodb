using LetsGetChecked.IlabUsers.Api.Domain.Enums;

namespace LetsGetChecked.IlabUsers.Api.Contracts.Requests;

public class UpdateUserRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Roles Role { get; set; }
}