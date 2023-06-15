using AutoMapper;
using LoanSystem.Api.DTO;
using LoanSystem.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AccountController(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationDto userRegistrationDto)
        {
            var user = _mapper.Map<User>(userRegistrationDto);

            var result = await _userManager.CreateAsync(user,userRegistrationDto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
            }

            await _userManager.AddToRoleAsync(user,"Member");
            return Ok();
        }
    }
}
