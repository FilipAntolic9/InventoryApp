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
            // Domain -> DTO
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Code, o => o.MapFrom(s => s.Code.Value));

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Order, OrderDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

            CreateMap<User, UserDto>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email.Value));

            CreateMap<Category, CategoryDto>();
            CreateMap<Supplier, SupplierDto>();

            // DTO -> Domain
            CreateMap<ProductDto, Product>()
                .ConstructUsing(d => new Product(
                    d.Id == Guid.Empty ? Guid.NewGuid() : d.Id,
                    ProductCode.Create(d.Code),
                    d.Name,
                    d.Description,
                    d.Price));

            CreateMap<OrderItemDto, OrderItem>()
                .ConstructUsing(d => new OrderItem(
                    d.ProductId,
                    d.ProductId,
                    d.Quantity,
                    d.UnitPrice));

            CreateMap<UserDto, User>()
                .ConstructUsing(d => new User(
                    d.Id == Guid.Empty ? Guid.NewGuid() : d.Id,
                    d.FirstName,
                    d.LastName,
                    EmailAddress.Create(d.Email),
                    null));

            CreateMap<CategoryDto, Category>()
                .ConstructUsing(d => new Category(
                    d.Id == Guid.Empty ? Guid.NewGuid() : d.Id,
                    d.Name));

            CreateMap<SupplierDto, Supplier>()
                .ConstructUsing(d => new Supplier(
                    d.Id == Guid.Empty ? Guid.NewGuid() : d.Id,
                    d.Name));
        }
    }
}