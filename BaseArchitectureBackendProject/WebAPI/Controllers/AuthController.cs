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
        public async Task<IActionResult> Register([FromForm] RegisterAuthDto user) //FromForm kullanma sebebimiz swagger üzerinden dosya yükleyebilmekti
        {
            var result=await _authService.RegisterAsync(user);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginAuthDto login)
        {
            var result =await _authService.LoginAsync(login);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}
