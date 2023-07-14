using LoanSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Contracts.Authorization.Responses
{
    public class LoanContextResponse
    {
        public Loan Loan { get; set; }
    }
}
