using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryApp.Application.DTOs;

namespace InventoryApp.Application.Services
{
    public interface ISupplierService
    {
        Task<List<SupplierDto>> GetAllAsync();
        Task<SupplierDto> GetByIdAsync(Guid id);
        Task<SupplierDto> CreateAsync(SupplierDto dto);
        Task UpdateAsync(SupplierDto dto);
        Task DeleteAsync(Guid id);
    }
}
