using FDXTestApp.Application.Contracts;
using FDXTestApp.Domain.Entities;
using MediatR;

namespace FDXTestApp.Application.Handlers
{
    public class GetSmsQueryHandler : IRequestHandler<GetSmsQuery, Sms>
    {
        private readonly ISmsRepository _smsRepository;

        public GetSmsQueryHandler(ISmsRepository smsRepository)
        {
            _smsRepository = smsRepository;
        }

        public async Task<Sms> Handle(GetSmsQuery request, CancellationToken cancellationToken)
        {
            var sms = await _smsRepository.GetSmsByIdAsync(request.Id, cancellationToken);

            if (sms == null)
            {
                return new Sms();
            }

            return sms;
        }
    }
}
