using FDXTestApp.Application.Contracts;
using FDXTestApp.Domain.Entities;
using MediatR;

namespace FDXTestApp.Application.Handlers
{
    public class GetSmsListQueryHandler : IRequestHandler<GetSmsListQuery, List<Sms>>
    {
        private readonly ISmsRepository _smsRepository;

        public GetSmsListQueryHandler(ISmsRepository smsRepository)
        {
            _smsRepository = smsRepository;
        }

        public async Task<List<Sms>> Handle(GetSmsListQuery request, CancellationToken cancellationToken)
        {
            var smsList = await _smsRepository.GetListSmsAsync();

            if (smsList == null)
            {
                return new List<Sms>();
            }

            return smsList;
        }
    }
}
