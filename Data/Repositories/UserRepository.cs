using Abstractions.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User> GetByNameAsync(string name)
    {
        var item = await Items.SingleOrDefaultAsync(e => e.Username == name);
        return GetItemOrThrowNullError(item, name, nameof(name));
    }

    public override async Task AddAsync(User item)
    {
        await ThrowIfUserAlreadyFound(item.Username);
        await base.AddAsync(item);
    }
    private async Task ThrowIfUserAlreadyFound(string username)
    {
        var foundUser = await Items
            .SingleOrDefaultAsync(user => user.Username == username);
        if (foundUser is not null)
            throw new Exception("Already found");
    }

}