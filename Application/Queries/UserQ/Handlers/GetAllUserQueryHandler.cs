using Application.Dtos;
using Application.Queries;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers;

public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, IEnumerable<UserDto>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public GetAllUserQueryHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.AsNoTracking().Select(x => new UserDto
        {
            Id = x.Id,
            Email = x.Email,
            UserName = x.UserName,
        }).ToListAsync(cancellationToken);

        return users;
    }
}