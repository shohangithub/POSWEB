using Domain.Enums;
using System.Linq.Expressions;

namespace POSWEB.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
//[ApiKey]
[Infrastructure.Authentication.Permission(ERoles.Admin)]
public class UsersController : ControllerBase
{
    private readonly IUserService<int> _userService;

    public UsersController(IUserService<int> userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IEnumerable<UserListResponse>> GetUsers(CancellationToken cancellationToken)
    {
        return await _userService.ListAsync(cancellationToken);
    }

    [HttpGet]
    [Route("Lookup")]
    public async Task<IEnumerable<Lookup<int>>> GetLookup(CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>> predicate = null;
        return await _userService.GetLookup(predicate, cancellationToken);
    }


    [HttpGet]
    [Route("GetWithPagination")]
    public async Task<PaginationResult<UserListResponse>> GetWithPagination([FromQuery] PaginationQuery requestQuery, CancellationToken cancellationToken)
    {
        return await _userService.PaginationListAsync(requestQuery, cancellationToken);
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetUser(int id, CancellationToken cancellationToken)
    {

        var user = await _userService.GetByIdAsync(id, cancellationToken);
        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponse>> PutUser(int id, UserRequest user)
    {
        var response = await _userService.UpdateAsync(id, user);
        return response;
    }

    [HttpPatch("{id}")]
    public IActionResult PatchUser(int id, [FromBody] JsonPatchDocument<UserRequest> patchDoc)
    {
        if (patchDoc != null)
        {
            dynamic obj = new ExpandoObject();
            patchDoc.ApplyTo(obj);


            return BadRequest(ModelState);
        }
        else
        {
            return BadRequest(ModelState);
        }
    }
     
    // POST: api/Users
    [HttpPost]
    public async Task<ActionResult<UserResponse>> PostUser(UserRequest user, CancellationToken cancellationToken)
    {
        return await _userService.AddAsync(user, cancellationToken);
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteUser(int id, CancellationToken cancellationToken)
    {
        return await _userService.DeleteAsync(id, cancellationToken);
    }

    private async ValueTask<bool> UserExists(int id, CancellationToken cancellationToken)
    {
        var response = await _userService.IsExistsAsync(id, cancellationToken);
        return response;
    }
}
