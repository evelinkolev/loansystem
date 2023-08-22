using LoanSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoanSystem.Infrastructure.Persistence
{
    public class LoanSystemContext : DbContext
    {
        public LoanSystemContext(DbContextOptions<LoanSystemContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Payer> Payers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfigurationsFromAssembly(typeof(LoanSystemContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
