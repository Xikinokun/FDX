using FDXTestApp.Application.Contracts;
using FDXTestApp.Domain.Entities;
using FDXTestApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FDXTestApp.Infrastructure.Repository
{
    public class SmsRepository : ISmsRepository
    {
        protected readonly Context _dbContext;

        public SmsRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Sms> AddSmsAsync(Sms sms, CancellationToken cancellationToken = default)
        {
            _dbContext.Sms.Add(sms);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return sms;
        }

        public async Task<List<Sms>> GetListSmsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Sms
                .Include(x => x.To)
                .AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Sms?> GetSmsByIdAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Sms
                .Include(x => x.To)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == guid, cancellationToken);
        }
    }
}
