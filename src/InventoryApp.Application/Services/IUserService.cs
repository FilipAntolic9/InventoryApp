using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryApp.Application.DTOs;

namespace InventoryApp.Application.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(Guid id);
        Task<UserDto> CreateAsync(UserDto dto, string password);
        Task UpdateAsync(UserDto dto);
        Task DeleteAsync(Guid id);
    }
}
