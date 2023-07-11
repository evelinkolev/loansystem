using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Contracts.Common.Responses
{
    public class FailResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
