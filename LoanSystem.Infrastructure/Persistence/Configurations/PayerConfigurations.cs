using LoanSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanSystem.Infrastructure.Persistence.Configurations
{
    internal sealed class PayerConfigurations : IEntityTypeConfiguration<Payer>
    {
        public void Configure(EntityTypeBuilder<Payer> builder)
        {
            ConfigurePayersTable(builder);
        }

        private void ConfigurePayersTable(EntityTypeBuilder<Payer> builder)
        {
            builder.ToTable("Payers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName).HasMaxLength(80);

            builder.Property(x => x.Deposit).HasPrecision(18, 4);

            builder.Property(x => x.RoutingNumber).HasMaxLength(8);

            builder.Property(x => x.AccountNumber).HasMaxLength(9);

            builder.Property(x => x.CreatedDateTime).HasPrecision(3);

            builder.Property(x => x.UpdatedDateTime).HasPrecision(3);
        }
    }
}
