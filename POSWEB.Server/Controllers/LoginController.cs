using Application.Contractors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace POSWEB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService<int> _userService;
        public LoginController(IUserService<int> userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var token = await _userService.GetUserToken(loginRequest.Email, cancellationToken);
            return Ok(token);
        }
    }
}
