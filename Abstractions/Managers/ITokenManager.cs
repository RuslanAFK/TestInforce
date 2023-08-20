using System.Security.Claims;
using Domain.Models;

namespace Abstractions.Managers;

public interface ITokenManager
{
    string GenerateToken(User foundUser);
    string GetUsernameOrThrow(ClaimsPrincipal? claimsPrincipal);
}