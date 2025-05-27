using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InventoryApp.Application.DTOs;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces;

namespace InventoryApp.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SupplierService(IUnitOfWork uow, IMapper mapper)
        {
            _uow    = uow;
            _mapper = mapper;
        }

        public async Task<List<SupplierDto>> GetAllAsync()
        {
            var list = await _uow.Repository<Supplier>().GetAllAsync();
            return _mapper.Map<List<SupplierDto>>(list);
        }

        public async Task<SupplierDto> GetByIdAsync(int id)
        {
            var ent = await _uow.Repository<Supplier>().GetByIdAsync(id);
            return _mapper.Map<SupplierDto>(ent);
        }

        public async Task<SupplierDto> CreateAsync(SupplierDto dto)
        {
            var ent = new Supplier(0, dto.Name);
            await _uow.Repository<Supplier>().AddAsync(ent);
            await _uow.SaveChangesAsync();
            return _mapper.Map<SupplierDto>(ent);
        }

        public async Task UpdateAsync(SupplierDto dto)
        {
            var repo = _uow.Repository<Supplier>();
            var ent  = await repo.GetByIdAsync(dto.Id);
            ent.ChangeName(dto.Name);
            repo.Update(ent);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var repo = _uow.Repository<Supplier>();
            var ent  = await repo.GetByIdAsync(id);
            repo.Remove(ent);
            await _uow.SaveChangesAsync();
        }
    }
}
