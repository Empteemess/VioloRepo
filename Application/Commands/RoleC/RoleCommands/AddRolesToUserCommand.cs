using Application.Dtos;
using MediatR;

namespace Application.Commands;

public record AddRolesToUserCommand(AddRolesToUserDto AddRolesToUserDto) : IRequest<bool>;
