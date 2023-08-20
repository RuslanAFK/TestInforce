using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class UrlDto
{
    public int Id { get; set; }
    public string FullAddress { get; set; }
    public string ShortAddress { get; set; }
}