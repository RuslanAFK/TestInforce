using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class AddUrlDto
{
    [Url]
    public string FullAddress { get; set; }
    public string Description { get; set; }
}