using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Contracts.Users.Response
{
    public class LoanResponse
    {
        public Guid Id { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal DownPayment { get; set; }
        public int LoanTermYears { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime RepaymentDate { get; set; }
        public string UserId { get; set; }
        public int Status { get; set; }
    }
}
