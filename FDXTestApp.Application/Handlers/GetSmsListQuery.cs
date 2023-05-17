using FDXTestApp.Domain.Entities;
using MediatR;

namespace FDXTestApp.Application.Handlers
{
    public class GetSmsListQuery: IRequest<List<Sms>>
    {
    }
}
