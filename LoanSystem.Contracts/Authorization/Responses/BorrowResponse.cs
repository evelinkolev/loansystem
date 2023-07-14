using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Contracts.Authorization.Responses
{
    public class BorrowResponse
    {
        public decimal LoanAmount { get; set; }
        public decimal MonthlyRepaymentAmount { get; set; }
        public DateTime NextRepaymentDate { get; set; }
    }
}
