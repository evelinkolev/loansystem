using LoanSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanSystem.Infrastructure.Persistence.Configurations
{
    internal sealed class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureUsersTable(builder);
        }

        private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).HasMaxLength(50);

            builder.Property(x => x.LastName).HasMaxLength(50);

            builder.Property(x =>x.Email).HasMaxLength(255);

            builder.Property(x => x.CreatedDateTime).HasPrecision(3);

            builder.Property(x => x.UpdatedDateTime).HasPrecision(3);
        }
    }
}
