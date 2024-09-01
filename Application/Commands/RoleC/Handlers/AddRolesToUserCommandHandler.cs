using Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.RoleC.Handlers;

public class AddRolesToUserCommandHandler : IRequestHandler<AddRolesToUserCommand, bool>
{
    private const string ModelMessage = "Model Doesn't Exists";
    private const string RoleMessage = "RoleC Doesn't Exists";
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AddRolesToUserCommandHandler(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<bool> Handle(AddRolesToUserCommand request, CancellationToken cancellationToken)
    {
        var userIds = request.AddRolesToUserDto.UserIds.ToList();

        if (userIds is null) throw new CustomException(ModelMessage);

        
        for (var i = 0; i < userIds.Count; i++)
        {
            var currentUser = await _userManager.FindByIdAsync(userIds[i].ToString());

            if (currentUser is null) throw new CustomException(ModelMessage);
            
            await _userManager.AddToRoleAsync(currentUser, nameof(request.AddRolesToUserDto.Role));
        }
        
        return true;
    }
}