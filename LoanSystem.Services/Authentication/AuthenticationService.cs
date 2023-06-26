using LoanSystem.Models.Domain;
using LoanSystem.Services.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthenticationOptions _authenticationOptions;

        public AuthenticationService(UserManager<User> userManager, AuthenticationOptions authenticationOptions)
        {
            _userManager = userManager;
            _authenticationOptions = authenticationOptions;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            // 1. Validate the user exists
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with given email does not exists." }
                };
            }

            // 2. Validate the password is correct
            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if(!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User/password combination is wrong." }
                };
            }

            return GenerateToken(user);
        }

        public async Task<AuthenticationResult> RegisterAsync(string firstName, string lastName, string email,string password)
        {
            // 1. Validate the user doesn't exists.
            var user = await _userManager.FindByEmailAsync(email);

            if(user != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with given email already exists." }
                };
            }

            // 2. Create new user.
            var newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(e => e.Description)
                };
            }

            return GenerateToken(newUser);
        }

        private AuthenticationResult GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authenticationOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.GivenName,user.FirstName),
                    new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim("role",user.Role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),                  
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationResult
            {
                Succes = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
