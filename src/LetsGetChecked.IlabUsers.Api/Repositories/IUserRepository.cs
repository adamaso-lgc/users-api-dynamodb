using LetsGetChecked.IlabUsers.Api.Domain.Entities;

namespace LetsGetChecked.IlabUsers.Api.Repositories;

public interface IUserRepository
{
    Task<bool> CreateAsync(User user);

    Task<User?> GetAsync(Guid id);

    Task<bool> UpdateAsync(User user);

    Task<bool> DeleteAsync(Guid id);
}