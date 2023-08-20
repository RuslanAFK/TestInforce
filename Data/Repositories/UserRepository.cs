using Abstractions.Repositories;
using Domain.Models;

namespace Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User> GetByNameAsync(string name)
    {
        return await GetBy(e => e.Username == name);
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await GetBy(e => e.Id == id);
    }

    public override async Task AddAsync(User item)
    {
        await CheckIfAlreadyFound(i => i.Username == item.Username);
        await base.AddAsync(item);
    }
}