using Domain.Models;

namespace Abstractions.Managers;

public interface ITokenManager
{
    string GenerateToken(User foundUser);
}