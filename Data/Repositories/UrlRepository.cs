using Abstractions.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UrlRepository : BaseRepository<Url>, IUrlRepository
{
    public UrlRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Url>> GetAllAsync()
    {
        return await Items.ToListAsync();
    }

    public async Task<Url> GetByIdAsync(int id)
    {
        return await GetByAsync(u => u.Id == id);
    }

    public async Task<Url> GetByShortAddressAsync(string shortAddress)
    {
        return await GetByAsync(u => u.ShortAddress == shortAddress);
    }

    public override async Task AddAsync(Url item)
    {
        await CheckIfAlreadyFoundAsync(i => i.FullAddress == item.FullAddress);
        await CheckIfAlreadyFoundAsync(i => i.ShortAddress == item.ShortAddress);
        await base.AddAsync(item);
    }
}