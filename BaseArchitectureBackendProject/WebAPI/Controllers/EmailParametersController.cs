using Bussiness.Repositories.EmailParameterRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailParametersController : ControllerBase
    {
        private readonly IEmailParameterService _emailParameterService;

        public EmailParametersController(IEmailParameterService emailParameterService)
        {
            _emailParameterService = emailParameterService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(EmailParameters emailParameter)
        {
            var result = await _emailParameterService.AddAsync(emailParameter);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(EmailParameters emailParameter)
        {
            var result = await _emailParameterService.UpdateAsync(emailParameter);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(EmailParameters emailParameter)
        {
            var result = await _emailParameterService.DeleteAsync(emailParameter);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            var result = await _emailParameterService.GetListAsync();

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> Get(int id)
        {
            var result =await _emailParameterService.GetByIdAsync(id);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}
