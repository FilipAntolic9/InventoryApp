using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryApp.Domain.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetByIdAsync(
            object id,
            CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllAsync(
            CancellationToken cancellationToken = default);

        IQueryable<TEntity> Query();

        Task AddAsync(
            TEntity entity,
            CancellationToken cancellationToken = default);

        void Update(TEntity entity);

        void Remove(TEntity entity);
    }
}
