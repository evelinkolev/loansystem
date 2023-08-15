using LoanSystem.Application.Abstraction.IAuthManager;
using LoanSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Infrastructure.Auth
{
    public partial class AuthManager : IAuthManager
    {
        private readonly JwtOptions _jwtOptions;

        public AuthManager(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public string GenerateToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
