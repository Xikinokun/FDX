using FDXTestApp.Domain.Entities;

namespace FDXTestApp.Application.Contracts
{
    public interface ISmsRepository
    {
        public Task<Sms> AddSmsAsync(Sms sms, CancellationToken cancellationToken = default);

        public Task<Sms?> GetSmsByIdAsync(Guid guid, CancellationToken cancellationToken = default);

        public Task<List<Sms>> GetListSmsAsync(CancellationToken cancellationToken = default);
    }
}
