namespace Application.ReponseDTO;

public record struct TokenResponse(string? Email, string? UserName, string Token);
