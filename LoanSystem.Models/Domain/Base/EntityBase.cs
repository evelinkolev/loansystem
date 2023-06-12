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
        protected EntityBase()
        {
            State = State.Unchanged;
        }

        public static T CreateInstance<T>(State state) where T : EntityBase, new()
        {
            T instance = new T
            {
                State = state
            };
            return instance;
        }

        public int Id { get; set; }

        [NotMapped]
        public State State { get; set; }
    }
}
