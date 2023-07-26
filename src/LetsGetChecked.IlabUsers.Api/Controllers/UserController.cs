using LetsGetChecked.IlabUsers.Api.Contracts.Requests;
using LetsGetChecked.IlabUsers.Api.Mappers;
using LetsGetChecked.IlabUsers.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LetsGetChecked.IlabUsers.Api.Controllers;

[ApiController]
[Route("api/v1/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody]CreateUserRequest request)
    {
        var result = await _userService.CreateAsync(request);
        
        return result.Match<IActionResult>(
            userResponse => CreatedAtAction(nameof(Get), new { id = userResponse.Id }, userResponse),
            failed => BadRequest(failed.ToResponse()));
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute]string id)
    {
        var result = await _userService.GetAsync(Guid.Parse(id));
        
        return result.Match<IActionResult>(
            userResponse => Ok(userResponse),
            _ => NotFound());
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody]UpdateUserRequest request)
    {
        var result = await _userService.UpdateAsync(Guid.Parse(id), request);
        
        return result.Match<IActionResult>(
            userResponse => Ok(userResponse),
            _ => NotFound(),
            failed => BadRequest(failed.ToResponse()));
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute]string id)
    {
        var result = await _userService.DeleteAsync(Guid.Parse(id));
        
        return result.Match<IActionResult>(
            _ => Ok(),
            _ => NotFound());
    }
    
}