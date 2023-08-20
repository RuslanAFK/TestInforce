using Abstractions.Managers;
using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Exceptions;
using Domain.Models;

namespace Api.Services;

public class UrlService : IUrlService
{
    private readonly IUrlRepository _urlRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IShortenManager _shortenManager;

    public UrlService(IUrlRepository urlRepository, IUnitOfWork unitOfWork, IShortenManager shortenManager)
    {
        _urlRepository = urlRepository;
        _unitOfWork = unitOfWork;
        _shortenManager = shortenManager;
    }
    public async Task<List<Url>> GetAllUrlsAsync()
    {
        var urls = await _urlRepository.GetAllAsync();
        return urls;
    }

    public async Task<Url> GetUrlByIdAsync(int id)
    {
        var url = await _urlRepository.GetByIdAsync(id);
        return url;
    }

    public async Task<Url> GetUrlByTokenAsync(string token)
    {
        var shortAddress = _shortenManager.Prefix + token;
        var url = await _urlRepository.GetByShortAddressAsync(shortAddress);
        return url;
    }

    public async Task AddUrlAsync(Url url, User user)
    {
        var urls = await _urlRepository.GetAllAsync();
        url = _shortenManager.Shorten(url, urls);
        url.CreatedDate = DateTime.Now;
        url.UserId = user.Id;
        await _urlRepository.AddAsync(url);
        await _unitOfWork.CompleteOrThrowAsync();
    }

    public async Task DeleteUrlAsync(Url url, User user)
    {
        if (!IsDeletingAllowed(user, url))
        {
            throw new UserNotAuthorizedException();
        }
        _urlRepository.Remove(url);
        await _unitOfWork.CompleteOrThrowAsync();
    }
    private bool IsDeletingAllowed(User user, Url url)
    {
        return user.IsAdmin || user.Id == url.UserId;
    }
}
