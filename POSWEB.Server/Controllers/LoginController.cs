using Application.Contractors;
using Infrastructure.Authentication;
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
        [ApiKey]
        [HttpPost]
        public async Task<TokenResponse> Post([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            return await _userService.GetUserToken(loginRequest.Email, cancellationToken);
        }
    }
}
