using Domain.Models;

namespace Abstractions.Services;

public interface IUrlService
{
    Task<List<Url>> GetAllUrls();
    Task<Url> GetUrlById(int id);
    Task<Url> GetUrlByToken(string token);
    Task AddUrl(Url url, User user);
    Task DeleteUrl(int id);
}