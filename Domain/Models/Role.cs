using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Role
{
    [Key]
    public int Id { get; set; }
    public string RoleName { get; set; }
    public ICollection<User> Users { get; set; }

    public Role()
    {
        Users = new Collection<User>();
    }
}