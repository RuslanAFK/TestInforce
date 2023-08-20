using Domain.Models;

namespace Abstractions.Repositories;

public interface IUserRepository
{
    Task AddAsync(User userToCreate);
    Task<User> GetByNameAsync(string userUsername);
    Task<User> GetByIdAsync(int id);
}