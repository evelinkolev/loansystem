﻿using LoanSystem.Models.Domain.Base;

namespace LoanSystem.Models.Domain
{
    public partial class User : EntityBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
}
