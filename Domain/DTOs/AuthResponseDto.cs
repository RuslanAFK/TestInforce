namespace Domain.DTOs;

public class AuthResponseDto
{
    public string Username { get; set; }
    public bool IsAdmin { get; set; }
    public string Token { get; set; }
}