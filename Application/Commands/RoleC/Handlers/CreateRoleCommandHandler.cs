using Application.Commands;
using Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
{
    private const string ErrorMessage = "RoleExists";
    private readonly RoleManager<IdentityRole> _roleManager;

    public CreateRoleCommandHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var checkRole = await _roleManager.FindByNameAsync(request.RoleName);
        
        if (checkRole is not null) throw new CustomException(ErrorMessage);
        
        await _roleManager.CreateAsync(new IdentityRole(request.RoleName));
        
        return Unit.Value;
    }
}