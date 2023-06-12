using LoanSystem.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Data
{
    public class LoanSystemContext : IdentityDbContext
    {
        public LoanSystemContext(DbContextOptions<LoanSystemContext> options) : base(options)
        {
        }
        public DbSet<Loan> Loan { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfigurationsFromAssembly(typeof(LoanSystemContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
