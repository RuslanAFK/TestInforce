using System.Linq.Expressions;
using Abstractions.Repositories;
using Domain.Exceptions;
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

    protected virtual async Task<TEntity> GetBy(Expression<Func<TEntity, bool>> expression)
    {
        var item = await Items.SingleOrDefaultAsync(expression);
        return GetItemOrThrowNullError(item);
    }

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

    private TEntity GetItemOrThrowNullError(TEntity? item)
    {
        if (item == null)
            throw new NotFoundException();
        return item;
    }

    protected virtual async Task CheckIfAlreadyFound(Expression<Func<TEntity, bool>> expression)
    {
        var foundUser = await Items.SingleOrDefaultAsync(expression);
        if (foundUser is not null)
            throw new AlreadyFoundException();
    }
}