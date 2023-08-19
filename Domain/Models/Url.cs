using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Url
{
    [Key]
    public int Id { get; set; }
    [Url]
    public string FullAddress { get; set; }
    [Url]
    public string ShortAddress { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UserId { get; set; }
}