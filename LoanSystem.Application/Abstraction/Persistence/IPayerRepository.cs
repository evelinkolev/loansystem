﻿using LoanSystem.Models.Domain;

namespace LoanSystem.Application.Abstraction.Persistence
{
    public interface IPayerRepository
    {
        Task CreateAsync(Payer payer);
        Task<Payer?> GetAsync(Guid id);
        Task UpdateAsync(Payer payer);
        Task<bool> UserHavePayerAsync(Guid payerId, Guid userId);
    }
}
