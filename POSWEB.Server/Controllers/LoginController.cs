using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using POSWEB.Server.ServiceContracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POSWEB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IUserService _userService;
        public LoginController(IConfiguration config,IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest,CancellationToken cancellationToken)
        {
            var token = await _userService.GetUserToken(loginRequest.Email, cancellationToken);
            return Ok(token);
        }
    }
}
