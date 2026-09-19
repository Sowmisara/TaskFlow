using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTO.Auth;
using TaskFlow.Application.IService;

namespace TaskFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromForm]RegisterDTO register)
        {
             await _service.Register(register);
             return Ok();
        }
    }
}
