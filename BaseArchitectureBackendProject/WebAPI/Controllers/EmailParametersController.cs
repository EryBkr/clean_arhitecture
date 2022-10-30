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
        public IActionResult Add(EmailParameters emailParameter)
        {
            var result = _emailParameterService.Add(emailParameter);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("update")]
        public IActionResult Update(EmailParameters emailParameter)
        {
            var result = _emailParameterService.Update(emailParameter);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(EmailParameters emailParameter)
        {
            var result = _emailParameterService.Delete(emailParameter);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("getList")]
        public IActionResult GetList()
        {
            var result = _emailParameterService.GetList();

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = _emailParameterService.GetById(id);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}
