﻿using Abstractions.Services;
using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        var urls = await _urlService.GetAllUrlsAsync();
        var dto = _mapper.Map<List<Url>, List<UrlDto>>(urls);
        return Ok(dto);
    }
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUrlDetails(int id)
    {
        var url = await _urlService.GetUrlByIdAsync(id);
        var dto = _mapper.Map<Url, UrlWithDetailsDto>(url);
        var owner = await _authService.GetUserByUserIdAsync(url.UserId);
        dto.CreatedBy = owner.Username;
        return Ok(dto);
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ShortenUrlAndPutInDb(AddUrlDto dto)
    {
        var url = _mapper.Map<AddUrlDto, Url>(dto);
        var user = await _authService.GetUserByClaimsPrincipalAsync(HttpContext?.User);
        await _urlService.AddUrlAsync(url, user);
        return NoContent();
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUrl(int id)
    {
        var user = await _authService.GetUserByClaimsPrincipalAsync(HttpContext?.User);
        var url = await _urlService.GetUrlByIdAsync(id);
        await _urlService.DeleteUrlAsync(url, user);
        return NoContent();
    }

    

    [HttpGet("/u.sho/{token}")]
    public async Task<IActionResult> MakeRedirect(string token)
    {
        var url = await _urlService.GetUrlByTokenAsync(token);
        return Redirect(url.FullAddress);
    }
}
