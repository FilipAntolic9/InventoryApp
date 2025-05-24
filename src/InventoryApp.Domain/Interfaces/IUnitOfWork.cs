using System.Threading;
using System.Threading.Tasks;

namespace InventoryApp.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>() 
            where TEntity : class;

        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
