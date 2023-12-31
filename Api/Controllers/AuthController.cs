﻿using Abstractions.Services;
using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    public AuthController(IMapper mapper, IAuthService authService)
    {
        _mapper = mapper;
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto loginResource)
    {
        var user = _mapper.Map<LoginDto, User>(loginResource);
        var (foundUser, token) = await _authService.GetAuthUserAndTokenAsync(user);
        var response = _mapper.Map<User, AuthResponseDto>(foundUser);
        response.Token = token;
        return Ok(response);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDto registerResource)
    {
        var userToCreate = _mapper.Map<RegisterDto, User>(registerResource);
        await _authService.RegisterAsync(userToCreate);
        return NoContent();
    }
}