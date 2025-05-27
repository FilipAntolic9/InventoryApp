using System;
using System.Linq;
using System.Threading.Tasks;
using InventoryApp.Application.DTOs;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces;

namespace InventoryApp.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _uow;

        public InventoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task AdjustAsync(InventoryAdjustDto dto)
        {
            // pronadi/napravi InventoryLocation
            var repo = _uow.Repository<InventoryLocation>();
            var existing = repo.Query()
                .FirstOrDefault(x => x.ProductId == dto.ProductId
                                  && x.Location   == dto.Location);

            if (existing == null)
            {
                existing = new InventoryLocation(
                    0,
                    dto.ProductId,
                    0,
                    dto.Location);
                await repo.AddAsync(existing);
            }

            existing.AdjustQuantity(dto.Delta);
            repo.Update(existing);
            await _uow.SaveChangesAsync();
        }
    }
}
