namespace Data.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity item);
    void Update(TEntity item);
    void Remove(TEntity item);
}