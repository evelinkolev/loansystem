﻿using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoanSystem.Infrastructure.Persistence.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly LoanSystemContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(LoanSystemContext context)
        {
            _context = context;
            _users = _context.Users;
        }
        public async Task AddAsync(User user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetAsync(string email)
        {
            return await _users.AsNoTracking().SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetAsync(Guid id)
        {
            return await _users.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(User user)
        {
            _users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
