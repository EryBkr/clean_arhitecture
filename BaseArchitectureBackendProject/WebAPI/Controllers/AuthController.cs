using Bussiness.Authentication;
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
        public IActionResult Register([FromForm] RegisterAuthDto user) //FromForm kullanma sebebimiz swagger üzerinden dosya yükleyebilmekti
        {
            var result=_authService.Register(user);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginAuthDto login)
        {
            var result = _authService.Login(login);
            return Ok(result);
        }
    }
}
