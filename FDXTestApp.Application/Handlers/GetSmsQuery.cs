using FDXTestApp.Domain.Entities;
using MediatR;

namespace FDXTestApp.Application.Handlers
{
    public class GetSmsQuery : IRequest<Sms>
    {
        public Guid Id { get; set; }
    }
}
