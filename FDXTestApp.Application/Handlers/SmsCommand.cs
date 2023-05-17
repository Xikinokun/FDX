using FDXTestApp.Domain.Enums;
using MediatR;

namespace FDXTestApp.Application.Handlers
{
    public record SmsCommand(Guid Id, string From, string[] To, string Content, StatusEnum Status) : IRequest<Unit>;
}
