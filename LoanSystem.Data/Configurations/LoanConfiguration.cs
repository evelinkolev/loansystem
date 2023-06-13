using LoanSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Data.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder
                .HasKey(x => new { x.Id });

            builder
                .HasData(
                new Loan { Id = 1, Name = "Personal Loan", Amount = 2000, TermInMonths = 2, InterestRate = 3.7, State = State.Rejected, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },
                new Loan { Id = 2, Name = "Personal Loan", Amount = 4000, TermInMonths = 4, InterestRate = 6.9, State = State.Approved, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },
                new Loan { Id = 3, Name = "Personal Loan", Amount = 6000, TermInMonths = 12, InterestRate = 15.4, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },
                new Loan { Id = 4, Name = "Personal Loan", Amount = 8000, TermInMonths = 24, InterestRate = 18.9, State = State.Submitted, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },

                new Loan { Id = 5, Name = "Business Loan", Amount = 390000, TermInMonths = 48, InterestRate = 7.7, State = State.Rejected, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },
                new Loan { Id = 6, Name = "Business Loan", Amount = 270000, TermInMonths = 392, InterestRate = 36.9, State = State.Approved, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },
                new Loan { Id = 7, Name = "Business Loan", Amount = 450000, TermInMonths = 86, InterestRate = 15.4, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },
                new Loan { Id = 8, Name = "Business Loan", Amount = 1013000, TermInMonths = 196, InterestRate = 18.9, State = State.Submitted, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },

                new Loan { Id = 9, Name = "Online Cash Loan", Amount = 3000, TermInMonths = 24, InterestRate = 4.7, State = State.Rejected, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },
                new Loan { Id = 10, Name = "Online Cash Loan", Amount = 2800, TermInMonths = 1, InterestRate = 2.8, State = State.Approved, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },
                new Loan { Id = 11, Name = "Online Cash Loan", Amount = 1700, TermInMonths = 2, InterestRate = 3.8, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },
                new Loan { Id = 12, Name = "Online Cash Loan", Amount = 2200, TermInMonths = 6, InterestRate = 5.9, State = State.Submitted, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },

                new Loan { Id = 13, Name = "Cash Advance", Amount = 900, TermInMonths = 1, InterestRate = 3, State = State.Rejected, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },
                new Loan { Id = 14, Name = "Cash Advance", Amount = 700, TermInMonths = 3, InterestRate = 3.1, State = State.Approved, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },
                new Loan { Id = 15, Name = "Cash Advance", Amount = 500, TermInMonths = 3, InterestRate = 3.1, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" },
                new Loan { Id = 16, Name = "Cash Advance", Amount = 300, TermInMonths = 1, InterestRate = 3, State = State.Submitted, UserId = "3eb97628-c518-4b41-845e-9f3f68ac51c9" }


                );
        }
    }
}
