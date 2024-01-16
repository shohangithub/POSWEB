using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Application.ReponseDTO;

public record class UserResponse(int Id, string UserName, string Email, ERoles Role, bool IsActive, string Status);
public record class UserListResponse(int Id, string UserName, string Email, ERoles Role, string Status);
