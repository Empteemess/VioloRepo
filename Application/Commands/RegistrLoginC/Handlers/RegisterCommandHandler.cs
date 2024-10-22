using Application.Commands;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterCommandHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = request.RegisterDto;
        
        if (user is null) throw new  ArgumentNullException();
        
        var wholeUser = new IdentityUser
        {
            Email = user.Email,
            UserName = user.Email,
        };

        var create = await _userManager.CreateAsync(wholeUser,user.Password);

        var createdUser = await _userManager.FindByEmailAsync(user.Email);
        await _userManager.AddToRoleAsync(createdUser, nameof(RoleEnums.User));

        return create.Succeeded;
    }
}