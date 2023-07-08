using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Models.Domain.Base
{
    public abstract class EntityBase
    {
        protected const int MonthsPerYear = 12;

        public Guid Id { get; set; }

        public State State { get; set; }
    }
}
