using FDXTestApp.Domain.Entities;
using FDXTestApp.Domain.Enums;
using MediatR;

namespace FDXTestApp.Application.Handlers
{
    public record SmsCreatedCommand(Guid Id, string From, string[] To, string Content, StatusEnum Status) : IRequest;
}
