using Application.Dtos;
using MediatR;

namespace Application.Commands;

public record RegisterCommand(RegisterDto RegisterDto) : IRequest<bool>;
