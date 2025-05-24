using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InventoryApp.Application.DTOs;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces;

namespace InventoryApp.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork uow, IMapper mapper)
        {
            _uow    = uow;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await _uow.Repository<Order>().GetAllAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetByIdAsync(Guid id)
        {
            var o = await _uow.Repository<Order>().GetByIdAsync(id);
            return _mapper.Map<OrderDto>(o);
        }

        public async Task<OrderDto> CreateAsync(OrderDto dto)
        {
            var order = new Order(Guid.NewGuid(), dto.UserId);

            foreach(var item in dto.Items)
                order.AddItem(
                    _uow.Repository<Product>().GetByIdAsync(item.ProductId).Result,
                    item.Quantity,
                    item.UnitPrice);

            await _uow.Repository<Order>().AddAsync(order);
            await _uow.SaveChangesAsync();
            return _mapper.Map<OrderDto>(order);
        }

        public async Task ChangeStatusAsync(Guid orderId, string newStatus)
        {
            var o = await _uow.Repository<Order>().GetByIdAsync(orderId);
            if (Enum.TryParse<OrderStatus>(newStatus, true, out var st))
                o.ChangeStatus(st);
            else
                throw new ArgumentException("ERROR: Invalid status.", nameof(newStatus));

            _uow.Repository<Order>().Update(o);
            await _uow.SaveChangesAsync();
        }
    }
}
