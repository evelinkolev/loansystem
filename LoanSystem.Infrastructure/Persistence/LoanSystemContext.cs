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
        public DbSet<Card> Cards { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfigurationsFromAssembly(typeof(LoanSystemContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
