using System;
using AutoMapper;
using InventoryApp.Application.DTOs;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.ValueObjects;

namespace InventoryApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Order, OrderDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

            CreateMap<User, UserDto>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email.Value));

            CreateMap<Supplier, SupplierDto>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<OrderItemDto, OrderItem>()
                .ConstructUsing(d => new OrderItem(
                    d.OrderId,
                    d.ProductId,
                    d.Quantity,
                    d.UnitPrice));

            CreateMap<UserDto, User>()
                .ConstructUsing(d => new User(
                    d.Id == 0 ? 0 : d.Id,
                    d.FirstName,
                    d.LastName,
                    EmailAddress.Create(d.Email),
                    d.Password));

            CreateMap<SupplierDto, Supplier>()
                .ConstructUsing(d => new Supplier(
                    d.Id == 0 ? 0 : d.Id,
                    d.Name));
        }
    }
}
