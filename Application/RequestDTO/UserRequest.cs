using Domain.Enums;

namespace Application.RequestDTO;

public class UserRequest
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public ERoles Role { get; set; }
    public required bool IsActive { get; set; }
}
