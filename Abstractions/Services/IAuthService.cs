using Domain.Models;
using System.Security.Claims;

namespace Abstractions.Services;

public interface IAuthService
{
    Task RegisterAsync(User userToCreate);
    Task<(User, string)> GetAuthUserAndTokenAsync(User user);
    Task<User> GetUserByClaimsPrincipalAsync(ClaimsPrincipal? claimsPrincipal);
    Task<User> GetUserByUserIdAsync(int userId);
}