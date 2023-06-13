using LoanSystem.Models.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Models.Domain
{
    public partial class Loan : EntityBase
    {
        public static Loan CreateInstance(State state = State.Submitted)
        {
            return CreateInstance<Loan>(state);
        }

        public string? Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public int TermInMonths { get; set; }
        public double InterestRate { get; set; }
        public string? UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
