using Domain.Models;

namespace Abstractions.Managers;

public interface IShortenManager
{
    string Token { get; set; }
    string Prefix { get; }
    Url Shorten(Url url, List<Url> urls);
}