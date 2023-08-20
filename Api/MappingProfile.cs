using AutoMapper;
using Domain.DTOs;
using Domain.Models;

namespace Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginDto, User>();
        CreateMap<RegisterDto, User>();
    }
}
