using Bussiness.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterAuthDto user)
        {
            var result=_authService.Register(user);
            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginAuthDto login)
        {
            var result = _authService.Login(login);
            return Ok(result);
        }
    }
}
