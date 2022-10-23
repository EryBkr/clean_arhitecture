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
        public IActionResult Add(OperationClaim claim)
        {
            var result=_operationClaimService.Add(claim);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("update")]
        public IActionResult Update(OperationClaim claim)
        {
            var result = _operationClaimService.Update(claim);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(OperationClaim claim)
        {
            var result = _operationClaimService.Delete(claim);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("getList")]
        public IActionResult GetList()
        {
            var result = _operationClaimService.GetList();

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = _operationClaimService.GetById(id);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}
