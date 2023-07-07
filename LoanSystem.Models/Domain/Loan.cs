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
        /// <summary>
        /// Indicates number of months per years used in calculations.
        /// </summary>
        private const int _MonthsPerYear = 12;

        public static Loan CreateInstance(State state = State.Submitted)
        {
            return CreateInstance<Loan>(state);
        }

        /// <summary>
        /// The total purchase price of the item being paid for.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// The total down payment towards the item being purchased.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal DownPayment { get; set; }

        /// <summary>
        /// The total loan amount. This is the purchase price less
        /// any down payment.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal LoanAmount
        {
            get { return PurchasePrice - DownPayment; }
        }

        /// <summary>
        /// The term of the loan in months. This is the number of months
        /// that payments will be made.
        /// </summary>
        public int LoanTermMonths { get; set; }

        /// <summary>
        /// The term of the loan in years. This is the number of years
        /// that payments will be made.
        /// </summary>
        public int LoanTermYears
        {
            get { return LoanTermMonths / _MonthsPerYear; }
            set { LoanTermMonths = value * _MonthsPerYear; }
        }

        /// <summary>
        /// The annual percentage rate (APR) includes the yearly cost of borrowing money.
        /// </summary>
        public decimal InterestRate { get; set; }

        /// <summary>
        /// Schedule date of the loan repayment.
        /// </summary>
        public DateTime RepaymentDate { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

    }
}
