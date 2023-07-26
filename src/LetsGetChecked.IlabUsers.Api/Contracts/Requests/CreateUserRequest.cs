using LetsGetChecked.IlabUsers.Api.Domain.Enums;

namespace LetsGetChecked.IlabUsers.Api.Contracts.Requests;

public class CreateUserRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; }
}