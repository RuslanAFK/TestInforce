using Domain.Models;

namespace Abstractions.Repositories;

public interface IUrlRepository
{
    Task<List<Url>> GetAllAsync();
    Task<Url> GetByIdAsync(int id);
    Task<Url> GetByShortAddressAsync(string shortName);
    Task AddAsync(Url url);
    void Remove(Url url);
}