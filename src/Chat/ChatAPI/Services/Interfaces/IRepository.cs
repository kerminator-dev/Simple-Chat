namespace ChatAPI.Services.Interfaces
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class
    {
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task<TEntity> Get(TKey key);
    }
}
