using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryApp.Application.DTOs;

namespace InventoryApp.Application.Services
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllAsync();
        Task<OrderDto> GetByIdAsync(Guid id);
        Task<OrderDto> CreateAsync(OrderDto dto);
        Task ChangeStatusAsync(Guid orderId, string newStatus);
    }
}
