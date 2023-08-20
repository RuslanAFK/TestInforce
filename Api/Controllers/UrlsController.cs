using Abstractions.Services;
using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlsController : Controller
{
    private readonly IUrlService _urlService;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    public UrlsController(IUrlService urlService, IMapper mapper, IAuthService authService)
    {
        _urlService = urlService;
        _mapper = mapper;
        _authService = authService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUrls()
    {
        var urls = await _urlService.GetAllUrls();
        var dto = _mapper.Map<List<Url>, List<UrlDto>>(urls);
        return Ok(dto);
    }
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUrlDetails(int id)
    {
        var url = await _urlService.GetUrlById(id);
        var dto = _mapper.Map<Url, UrlWithDetailsDto>(url);
        return Ok(dto);
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ShortenUrlAndPutInDb(AddUrlDto dto)
    {
        var url = _mapper.Map<AddUrlDto, Url>(dto);
        var user = await _authService.GetUserByClaimsPrincipal(HttpContext?.User);
        await _urlService.AddUrl(url, user);
        return NoContent();
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUrl(int id)
    {
        var user = await _authService.GetUserByClaimsPrincipal(HttpContext?.User);
        
        if (user.IsAdmin || user.Urls.Any(u => u.Id == id))
        {
            await _urlService.DeleteUrl(id);
            return NoContent();
        }
        return Unauthorized();
    }

    [HttpGet("/u.sho/{token}")]
    public async Task<IActionResult> MakeRedirect(string token)
    {
        var url = await _urlService.GetUrlByToken(token);
        return Redirect(url.FullAddress);
    }
}
