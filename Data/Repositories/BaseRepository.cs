using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;

    protected BaseRepository(AppDbContext context)
    {
        _dbContext = context;
    }

    protected IQueryable<TEntity> Items => _dbContext.Set<TEntity>();

    public virtual async Task AddAsync(TEntity item)
    {
        await _dbContext.Set<TEntity>().AddAsync(item);
    }

    public virtual void Update(TEntity item)
    {
        _dbContext.Set<TEntity>().Update(item);
    }

    public virtual void Remove(TEntity item)
    {
        _dbContext.Set<TEntity>().Remove(item);
    }

    protected IQueryable<TEntity> GetItemsIncluding<TProperty>(IQueryable<TEntity> items,
        Expression<Func<TEntity, TProperty>> func)
    {
        return items.Include(func);
    }

    protected TEntity GetItemOrThrowNullError(TEntity? item, string propertyValue, string propertyName)
    {
        if (item == null)
            throw new Exception("Not Found");
        return item;
    }
}