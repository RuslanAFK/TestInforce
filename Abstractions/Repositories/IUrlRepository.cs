using Domain.Models;

namespace Abstractions.Repositories;

public interface IUrlRepository
{
    Task<List<Url>> GetAll();
    Task<Url> GetById(int id);
    Task<Url> GetByShortAddress(string shortName);
    Task AddAsync(Url url);
    void Remove(Url url);
}