using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryApp.Application.DTOs;

namespace InventoryApp.Application.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(Guid id);
        Task<CategoryDto> CreateAsync(CategoryDto dto);
        Task UpdateAsync(CategoryDto dto);
        Task DeleteAsync(Guid id);
    }
}
