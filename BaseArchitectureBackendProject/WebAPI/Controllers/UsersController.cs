using Bussiness.Repositories.UserRepository;
using Core.Utilities.Results.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var result =await _userService.GetListAsync();
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int userId)
        {
            var result = await _userService.GetByIdAsync(userId);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(User user)
        {
            var result = await _userService.UpdateAsync(user);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(User user)
        {
            var result = await _userService.DeleteAsync(user);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword(UserChangePasswordDto user)
        {
            var result = await _userService.ChangePasswordAsync(user);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}
