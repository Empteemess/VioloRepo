using Domain.Enums;

namespace Application.Dtos;

public class AddRolesToUserDto
{
    public required IEnumerable<string> UserIds { get; set; }

    public required RoleEnums Role { get; set; }
}