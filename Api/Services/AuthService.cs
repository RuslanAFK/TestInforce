using Abstractions.Managers;
using Abstractions.Repositories;
using Abstractions.Services;
using Domain.DTOs;
using Domain.Models;
using System.Security.Claims;

namespace Api.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _usersRepository;
    private readonly ITokenManager _tokenManager;
    private readonly IPasswordManager _passwordManager;
    private readonly IUnitOfWork _unitOfWork;
    public AuthService(IUserRepository usersRepository, ITokenManager tokenManager,
        IUnitOfWork unitOfWork, IPasswordManager passwordManager)
    {
        _usersRepository = usersRepository;
        _tokenManager = tokenManager;
        _unitOfWork = unitOfWork;
        _passwordManager = passwordManager;
    }
    public async Task RegisterAsync(User userToCreate)
    {
        _passwordManager.SecureUser(userToCreate);
        await _usersRepository.AddAsync(userToCreate);
        await _unitOfWork.CompleteOrThrowAsync();
    }
    public async Task<AuthResponseDto> GetAuthCredentialsAsync(User user)
    {
        var foundUser = await _usersRepository.GetByNameAsync(user.Username);
        _passwordManager.ThrowExceptionIfWrongPassword(user.Password, foundUser.Password);
        var token = _tokenManager.GenerateToken(foundUser);
        return new AuthResponseDto
        {
            Token = token,
            Username = foundUser.Username,
            RoleName = foundUser.IsAdmin ? "Admin" : "User"
        };
    }
    public async Task<User> GetUserByClaimsPrincipal(ClaimsPrincipal? claimsPrincipal)
    {
        var username = _tokenManager.GetUsernameOrThrow(claimsPrincipal);
        var user = await _usersRepository.GetByNameAsync(username);
        return user;
    }
}