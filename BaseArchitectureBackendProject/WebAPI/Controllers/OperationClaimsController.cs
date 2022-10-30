using Bussiness.Repositories.OperationClaimRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        private readonly IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(OperationClaim claim)
        {
            var result=await _operationClaimService.AddAsync(claim);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(OperationClaim claim)
        {
            var result = await _operationClaimService.UpdateAsync(claim);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(OperationClaim claim)
        {
            var result = await _operationClaimService.DeleteAsync(claim);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var result = await _operationClaimService.GetListAsync();

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _operationClaimService.GetByIdAsync(id);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}
