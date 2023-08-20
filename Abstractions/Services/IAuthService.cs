using Domain.DTOs;
using Domain.Models;

namespace Abstractions.Services;

public interface IAuthService
{
    Task RegisterAsync(User userToCreate);
    Task<AuthResponseDto> GetAuthCredentialsAsync(User user);
}