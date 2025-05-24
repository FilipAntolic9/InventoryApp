using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces;
using InventoryApp.Application.DTOs;

namespace InventoryApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork uow, IMapper mapper)
        {
            _uow    = uow;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _uow.Repository<Product>().GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var prod = await _uow.Repository<Product>().GetByIdAsync(id);
            return _mapper.Map<ProductDto>(prod);
        }

        public async Task<ProductDto> CreateAsync(ProductDto dto)
        {
            var prod = _mapper.Map<Product>(dto);
            await _uow.Repository<Product>().AddAsync(prod);
            await _uow.SaveChangesAsync();
            return _mapper.Map<ProductDto>(prod);
        }

        public async Task UpdateAsync(ProductDto dto)
        {
            var existing = await _uow.Repository<Product>().GetByIdAsync(dto.Id);
            _mapper.Map(dto, existing);
            _uow.Repository<Product>().Update(existing);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await _uow.Repository<Product>().GetByIdAsync(id);
            _uow.Repository<Product>().Remove(existing);
            await _uow.SaveChangesAsync();
        }
    }
}
