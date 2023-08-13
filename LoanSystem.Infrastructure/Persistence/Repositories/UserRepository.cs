using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
