using Abstractions.Managers;
using Abstractions.Repositories;
using Abstractions.Services;
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
    public async Task<List<Url>> GetAllUrls()
    {
        var urls = await _urlRepository.GetAll();
        return urls;
    }

    public async Task<Url> GetUrlById(int id)
    {
        var url = await _urlRepository.GetById(id);
        return url;
    }

    public async Task<Url> GetUrlByToken(string token)
    {
        var shortAddress = _shortenManager.Prefix + token;
        var url = await _urlRepository.GetByShortAddress(shortAddress);
        return url;
    }

    public async Task AddUrl(Url url, User user)
    {
        var urls = await _urlRepository.GetAll();
        url = _shortenManager.Shorten(url, urls);
        url.CreatedDate = DateTime.Now;
        url.UserId = user.Id;
        await _urlRepository.AddAsync(url);
        await _unitOfWork.CompleteOrThrowAsync();
    }

    public async Task DeleteUrl(int id)
    {
        
        var url = await _urlRepository.GetById(id);
        _urlRepository.Remove(url);
        await _unitOfWork.CompleteOrThrowAsync();
    }
}
