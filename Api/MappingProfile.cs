using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Routing.Tree;

namespace Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginDto, User>();
        CreateMap<RegisterDto, User>();
        CreateMap<User, AuthResponseDto>();

        CreateMap<AddUrlDto, Url>();
        CreateMap<Url, UrlDto>();
        CreateMap<Url, UrlWithDetailsDto>();
    }
}
