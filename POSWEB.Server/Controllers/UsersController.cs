﻿using Application.Common;

namespace POSWEB.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
//[ApiKey]
[Authorize(Permissions = "", Policies = "", Roles = "")]
public class UsersController : ControllerBase
{
    private readonly IUserService<int> _userService;

    public UsersController(IUserService<int> userService)
    {
        _userService = userService;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<IEnumerable<UserListResponse>> GetUsers(CancellationToken cancellationToken)
    {
        return await _userService.ListAsync(cancellationToken);
    }

    [HttpGet]
    [Route("GetPageUsers")]
    public async Task<PaginationResult<UserListResponse>> GetPageUsers([FromQuery]PaginationQuery requestQuery, CancellationToken cancellationToken)
    {
        Console.WriteLine(requestQuery);
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
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<UserResponse>> PostUser(UserRequest user, CancellationToken cancellationToken)
    {
        return await _userService.AddAsync(user, cancellationToken); ;
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
    {
        return NoContent();
    }

    private async ValueTask<bool> UserExists(int id, CancellationToken cancellationToken)
    {
        var response = await _userService.IsExistsAsync(id, cancellationToken);
        return response;
    }
}
