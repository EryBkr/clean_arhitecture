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
        public IActionResult GetList()
        {
            var result = _userService.GetList();
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int userId)
        {
            var result = _userService.GetById(userId);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("update")]
        public IActionResult Update(User user)
        {
            var result = _userService.Update(user);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(User user)
        {
            var result = _userService.Delete(user);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("changePassword")]
        public IActionResult ChangePassword(UserChangePasswordDto user)
        {
            var result = _userService.ChangePassword(user);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}
