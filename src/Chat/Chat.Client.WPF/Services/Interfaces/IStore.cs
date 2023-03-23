using System.Threading.Tasks;

namespace Chat.Client.WPF.Services.Interfaces
{
    internal interface IStore<TKey, TEntity> : IObservableStore<TKey, TEntity>
        where TEntity : class
    {
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task<TEntity> Get(TKey key);
    }
}
