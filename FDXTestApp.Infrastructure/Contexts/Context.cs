using FDXTestApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FDXTestApp.Infrastructure.Contexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }

        public DbSet<Sms> Sms => Set<Sms>();
        public DbSet<Recipient> Recipient => Set<Recipient>();
    }
}
