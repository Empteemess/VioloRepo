using Application.Dtos;
using MediatR;

namespace Application.Queries;

public record GetAllUserQuery : IRequest<IEnumerable<UserDto>>;
