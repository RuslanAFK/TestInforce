namespace Domain.DTOs;

public class RegisterDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
}