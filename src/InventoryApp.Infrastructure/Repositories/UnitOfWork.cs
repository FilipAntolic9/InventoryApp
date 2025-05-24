using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using InventoryApp.Domain.Interfaces;
using InventoryApp.Infrastructure.Data;

namespace InventoryApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories 
            = new ConcurrentDictionary<Type, object>();
        private bool _disposed = false;

        public UnitOfWork(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc />
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);
            return (IRepository<TEntity>) _repositories.GetOrAdd(type, 
                t => new Repository<TEntity>(_context));
        }

        /// <inheritdoc />
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
