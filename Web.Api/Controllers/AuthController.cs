using Application.Commands;
using Application.Dtos;
using Application.Queries;
using Application.Queries.AuthQ.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetToken(string email)
    {
        try
        {
            var query = new GetTokenQuery(email);
            var token = await _mediator.Send(query);
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllUserQuery();
        var users = await _mediator.Send(query);

        return Ok(users);
    }
    
    [HttpPost]
    [Route("{roleName}")]
    public async Task<IActionResult> AddRole(string roleName)
    {
        try
        {
            var command = new CreateRoleCommand(roleName);
            await _mediator.Send(command);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("role")]
    public async Task<IActionResult> AddRolesToUser([FromBody] AddRolesToUserDto addRolesToUserDto)
    {
        try
        {
            var command = new AddRolesToUserCommand(addRolesToUserDto);
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Added Successfully");
            }
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return BadRequest();
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var command = new RegisterCommand(registerDto);
            var result = await _mediator.Send(command);

            if (!result) return BadRequest("Error");

            return Ok("Added Successfully.");
        }
        catch (ArgumentNullException e)
        {
            return BadRequest(e.Message);
        }
    }
}