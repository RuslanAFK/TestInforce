using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class UrlDto
{
    public string FullAddress { get; set; }
    public string ShortAddress { get; set; }
}