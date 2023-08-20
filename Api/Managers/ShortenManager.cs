using Abstractions.Managers;
using Domain.Models;

namespace Api.Managers;

public class ShortenManager : IShortenManager
{
    public string Token { get; set; }
    public string Prefix { get; } = "https://localhost:7178/u.sho/";
    private void GenerateToken()
    {
        string urlsafe = string.Empty;
        Enumerable.Range(48, 75)
            .Where(i => i < 58 || i > 64 && i < 91 || i > 96)
            .OrderBy(_ => new Random().Next())
            .ToList()
            .ForEach(i => urlsafe += Convert.ToChar(i));
        Token = urlsafe.Substring(new Random().Next(0, urlsafe.Length), new Random().Next(2, 6));
    }

    public Url Shorten(Url url, List<Url> urls)
    {
        do
        {
            GenerateToken();
        } while (urls.Exists(u => u.ShortAddress
                     .Replace(Prefix, string.Empty)
                     .Equals(Token)));

        url.ShortAddress = Prefix + Token;
        return url;
    }
}
