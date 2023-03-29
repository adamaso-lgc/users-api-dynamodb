using FluentValidation.Results;
using LetsGetChecked.IlabUsers.Api.Contracts.Requests;
using LetsGetChecked.IlabUsers.Api.Contracts.Responses;
using LetsGetChecked.IlabUsers.Api.Domain.Entities;
using LetsGetChecked.IlabUsers.Api.Mappers;
using LetsGetChecked.IlabUsers.Api.Repositories;
using LetsGetChecked.IlabUsers.Api.Validators;
using Microsoft.AspNetCore.Identity;
using OneOf;
using OneOf.Types;


namespace LetsGetChecked.IlabUsers.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly UserValidator _userValidator;

    public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, UserValidator userValidator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _userValidator = userValidator;
    }

    public async Task<OneOf<UserResponse, ValidationFailed>> CreateAsync(CreateUserRequest request)
    {
        var user = request.ToUser();
        
        var validationResult = _userValidator.Validate(user);
        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }
        
        var userExists = await _userRepository.GetAsync(user.Id);
        if (userExists is not null)
        {
            var error = new ValidationFailure("UserId",$"A user with id {user.Id} already exists");
            return new ValidationFailed(error);
        }
        
        user.Password = _passwordHasher.HashPassword(user, request.Password);

        await _userRepository.CreateAsync(user);
        
        return user.ToUserResponse();

    }

    public async Task<OneOf<UserResponse, NotFound>> GetAsync(Guid id)
    {
        var user = await _userRepository.GetAsync(id);
        if (user is null)
        {
            return new NotFound();
        }
        
        return user.ToUserResponse();
    }

    public async Task<OneOf<UserResponse, NotFound, ValidationFailed>> UpdateAsync(Guid id, UpdateUserRequest request)
    {
        var user = request.ToUser(id);
        
        var validationResult = _userValidator.Validate(user);
        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }
        
        var userExists = await _userRepository.GetAsync(user.Id);
        if (userExists is null)
        {
            return new NotFound();
        }
        
        await _userRepository.UpdateAsync(user);

        return user.ToUserResponse();
    }

    public async Task<OneOf<Success, NotFound>> DeleteAsync(Guid id)
    {
        var userExists = await _userRepository.GetAsync(id);
        if (userExists is null)
        {
            return new NotFound();
        }
        
        await _userRepository.DeleteAsync(id);

        return new Success();
    }
    
}