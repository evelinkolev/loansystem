using LoanSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanSystem.Infrastructure.Persistence.Configurations
{
    internal sealed class CardConfigurations : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            ConfigureCardsTable(builder);
        }

        private void ConfigureCardsTable(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Number).HasMaxLength(16);

            builder.Property(x => x.HolderName).HasMaxLength(80);

            builder.Property(x => x.ExpiryDate).HasMaxLength(30);

            builder.Property(x => x.SecurityCode).HasMaxLength(3);

            builder.Property(x => x.CreatedDateTime).HasPrecision(3);

            builder.Property(x => x.UpdatedDateTime).HasPrecision(3);
        }
    }
}
