using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryApp.Application.DTOs;

namespace InventoryApp.Application.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(Guid id);
        Task<ProductDto> CreateAsync(ProductDto dto);
        Task UpdateAsync(ProductDto dto);
        Task DeleteAsync(Guid id);
    }
}
