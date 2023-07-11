using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Contracts.Users.Requests
{
    public class BorrowRequest
    {
        public decimal PurchasePrice { get; set; }
        public decimal DownPayment { get; set; }
        public int LoanTermYears { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime RepaymentDate { get; set; }
    }
}
