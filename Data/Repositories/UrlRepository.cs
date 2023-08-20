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
        return await Items.SingleOrDefaultAsync(u => u.Id == id)
            ?? throw new Exception("Not found");
    }

    public async Task<Url> GetByShortAddress(string shortName)
    {
        return await Items.SingleOrDefaultAsync(u => u.ShortAddress == shortName)
               ?? throw new Exception("Not found");
    }
}