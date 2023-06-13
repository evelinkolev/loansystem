﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Models.Domain
{
    public class User : IdentityUser
    {
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
