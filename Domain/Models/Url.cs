using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Url
{
    public int Id { get; set; }
    public string FullAddress { get; set; }
    public string ShortAddress { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UserId { get; set; }
}