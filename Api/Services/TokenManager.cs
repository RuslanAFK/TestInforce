using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Abstractions.Managers;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Api.Services;

public class TokenManager : ITokenManager
{
    private readonly IConfiguration _configuration;

    public TokenManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(Convert.FromBase64String(_configuration["Jwt:PrivateKey"]), out _);
        var securityKey = new RsaSecurityKey(rsa);

        var roleName = GetRoleName(user);
        var claims = GenerateClaims(user.Username, roleName);
        var token = GenerateTokenObject(claims, securityKey);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GetRoleName(User user)
    {
        return user.IsAdmin ? "Admin" : "User";
    }
    private JwtSecurityToken GenerateTokenObject(IEnumerable<Claim> claims, RsaSecurityKey securityKey)
    {
        var signingCredentials = GenerateSigningCredentials(securityKey);
        var jwtDate = GetCurrentTime();
        return new JwtSecurityToken(
            claims: claims,
            notBefore: jwtDate,
            expires: jwtDate.AddMinutes(60),
            signingCredentials: signingCredentials
        );
    }

    private SigningCredentials GenerateSigningCredentials(RsaSecurityKey securityKey)
    {
        var algorithm = SecurityAlgorithms.RsaSha256;
        var signingCredentials = new SigningCredentials(securityKey, algorithm)
        {
            CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
        };
        return signingCredentials;
    }
    private List<Claim> GenerateClaims(string username, string roleName)
    {
        var uniqueNameClaim = new Claim(JwtRegisteredClaimNames.UniqueName, username);
        var roleClaim = new Claim(ClaimTypes.Role, roleName);
        return new List<Claim> { uniqueNameClaim, roleClaim };
    }

    private DateTime GetCurrentTime()
    {
        return DateTime.Now;
    }

}