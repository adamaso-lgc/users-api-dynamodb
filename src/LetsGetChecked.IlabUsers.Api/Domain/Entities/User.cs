using System.Text.Json.Serialization;
using LetsGetChecked.IlabUsers.Api.Domain.Common;
using LetsGetChecked.IlabUsers.Api.Domain.Enums;

namespace LetsGetChecked.IlabUsers.Api.Domain.Entities;

public class User : Entity
{
    [JsonPropertyName("pk")]
    public string Pk => Id.ToString();

    [JsonPropertyName("sk")]
    public string Sk => Pk;
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; }
} 