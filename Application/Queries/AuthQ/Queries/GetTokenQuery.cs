using MediatR;

namespace Application.Queries.AuthQ.Queries;

public record GetTokenQuery(string Email) : IRequest<string>;
