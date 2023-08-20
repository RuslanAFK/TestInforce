using Abstractions.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UrlRepository : BaseRepository<Url>, IUrlRepository
{
    public UrlRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Url>> GetAll()
    {
        return await Items.ToListAsync();
    }

    public async Task<Url> GetById(int id)
    {
        return await GetBy(u => u.Id == id);
    }

    public async Task<Url> GetByShortAddress(string shortAddress)
    {
        return await GetBy(u => u.ShortAddress == shortAddress);
    }

    public override async Task AddAsync(Url item)
    {
        await CheckIfAlreadyFound(i => i.FullAddress == item.FullAddress);
        await CheckIfAlreadyFound(i => i.ShortAddress == item.ShortAddress);
        await base.AddAsync(item);
    }
}