using Bussiness.Repositories.UserOperationClaimRepository;
using Entities.Concrete;
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
        public IActionResult Add(UserOperationClaim claim)
        {
            var result = _userOperationClaimService.Add(claim);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("update")]
        public IActionResult Update(UserOperationClaim claim)
        {
            var result = _userOperationClaimService.Update(claim);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(UserOperationClaim claim)
        {
            var result = _userOperationClaimService.Delete(claim);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("getList")]
        public IActionResult GetList()
        {
            var result = _userOperationClaimService.GetList();

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = _userOperationClaimService.GetById(id);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}
