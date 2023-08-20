using Domain.Models;

namespace Abstractions.Services;

public interface IUrlService
{
    Task<List<Url>> GetAllUrlsAsync();
    Task<Url> GetUrlByIdAsync(int id);
    Task<Url> GetUrlByTokenAsync(string token);
    Task AddUrlAsync(Url url, User user);
    Task DeleteUrlAsync(Url url, User user);
}