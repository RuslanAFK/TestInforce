using System.Security.Cryptography;
using Abstractions.Managers;
using Abstractions.Repositories;
using Abstractions.Services;
using Api;
using Api.Managers;
using Api.Middleware;
using Api.Services;
using Data;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(o
    => o.UseSqlServer(builder.Configuration.GetConnectionString("default")));
builder.Services.AddAutoMapper(o => o.AddProfile<MappingProfile>());

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUrlService, UrlService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUrlRepository, UrlRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ITokenManager, TokenManager>();
builder.Services.AddScoped<IPasswordManager, PasswordManager>();
builder.Services.AddScoped<IShortenManager, ShortenManager>();

builder.Services.AddSingleton<RsaSecurityKey>(provider =>
{
    var rsa = RSA.Create();
    rsa.ImportRSAPublicKey(Convert.FromBase64String(builder.Configuration["Jwt:PublicKey"]), out _);
    return new RsaSecurityKey(rsa);
});
builder.Services.AddAuthentication(x =>
    {
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var rsa = builder.Services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();

        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = rsa,
            RequireSignedTokens = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(b =>
    {
        b.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:4200");
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors();

app.Run();
