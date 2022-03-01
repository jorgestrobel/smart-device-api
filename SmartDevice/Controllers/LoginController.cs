using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartDevice.Dto;
using SmartDevice.Services.Authentication;
using System.Threading.Tasks;

namespace SmartDevice.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Post(LoginDto loginVO)
        {
            if (loginVO == null)
            {
                return BadRequest();
            }
            AuthenticateResponse result = await loginService.Authenticate(loginVO, ipAddress());
            if (!result.Authenticated)
            {
                return BadRequest(result.Response);
            }
            else
            {
                return Ok(result.Response);
            }
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }


    }
}
