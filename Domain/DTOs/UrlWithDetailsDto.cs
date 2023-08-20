namespace Domain.DTOs;

public class UrlWithDetailsDto
{
    public string FullAddress { get; set; }
    public string ShortAddress { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
}