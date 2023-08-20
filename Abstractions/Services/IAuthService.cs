using Domain.DTOs;
using Domain.Models;
using System.Security.Claims;

namespace Abstractions.Services;

public interface IAuthService
{
    Task RegisterAsync(User userToCreate);
    Task<AuthResponseDto> GetAuthCredentialsAsync(User user);
    Task<User> GetUserByClaimsPrincipal(ClaimsPrincipal? claimsPrincipal);
}