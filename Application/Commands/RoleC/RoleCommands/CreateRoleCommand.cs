using MediatR;

namespace Application.Commands;

public record CreateRoleCommand(string RoleName) : IRequest;
