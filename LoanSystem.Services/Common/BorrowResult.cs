using LoanSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Services.Common
{
    public class BorrowResult
    {
        public decimal LoanAmount { get; set; }
        public decimal EMI { get; set; }
        public DateTime NextRepaymentDate { get; set; }
        public bool Succes { get; set; }
        public IEnumerable<string> Messages { get; set; }
    }
}
