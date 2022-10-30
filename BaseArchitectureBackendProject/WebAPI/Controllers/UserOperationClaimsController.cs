using Bussiness.Repositories.UserOperationClaimRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        private readonly IUserOperationClaimService _userOperationClaimService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(UserOperationClaim claim)
        {
            var result = await _userOperationClaimService.AddAsync(claim);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UserOperationClaim claim)
        {
            var result = await _userOperationClaimService.UpdateAsync(claim);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(UserOperationClaim claim)
        {
            var result = await _userOperationClaimService.DeleteAsync(claim);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var result = await _userOperationClaimService.GetListAsync();

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _userOperationClaimService.GetByIdAsync(id);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}
