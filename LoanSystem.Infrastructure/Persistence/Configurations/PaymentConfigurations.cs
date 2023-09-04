using LoanSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanSystem.Infrastructure.Persistence.Configurations
{
    internal sealed class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            ConfigurePaymentsTable(builder);
        }

        private void ConfigurePaymentsTable(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount).HasPrecision(18, 4);

            builder.Property(x => x.RequestType).HasMaxLength(8);

            builder.Property(x => x.CreatedDateTime).HasPrecision(3);

            builder.Property(x => x.UpdatedDateTime).HasPrecision(3);
        }
    }
}
