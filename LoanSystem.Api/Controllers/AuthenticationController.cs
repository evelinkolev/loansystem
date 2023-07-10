using LoanSystem.Contracts.Authentication.Requests;
using LoanSystem.Contracts.Authentication.Responses;
using LoanSystem.Services.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Api.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("api/authentication/register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest request)
        {
            var authResponse = await _authenticationService.RegisterAsync(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password); 

            if(!authResponse.Succes)
            {
                return BadRequest(new FailResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new SuccessResponse
            {
                Token = authResponse.Token
            });
        }

        [HttpPost("api/authentication/login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            var authResponse = await _authenticationService.LoginAsync(
                request.Email,
                request.Password);

            if (!authResponse.Succes)
            {
                return BadRequest(new FailResponse 
                {
                    Errors = authResponse.Errors 
                });
            }

            return Ok(new SuccessResponse 
            {
                Token = authResponse.Token 
            });
        }
    }
}
