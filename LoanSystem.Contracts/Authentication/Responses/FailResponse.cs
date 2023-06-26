using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Contracts.Authentication.Responses
{
    public class FailResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
