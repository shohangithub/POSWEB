namespace Application.RequestDTO;

public class UserRequest
{
    [Required]
    public required string UserName { get; set; }
    [Required]
    [EmailAddress]
    public required string? Email { get; set; }
    [Required]
    public ERoles Role { get; set; }
    [Required]
    public required bool IsActive { get; set; }
}
