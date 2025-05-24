using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InventoryApp.Application.DTOs;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces;

namespace InventoryApp.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow    = uow;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var list = await _uow.Repository<Category>().GetAllAsync();
            return _mapper.Map<List<CategoryDto>>(list);
        }

        public async Task<CategoryDto> GetByIdAsync(Guid id)
        {
            var ent = await _uow.Repository<Category>().GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(ent);
        }

        public async Task<CategoryDto> CreateAsync(CategoryDto dto)
        {
            var ent = new Category(Guid.NewGuid(), dto.Name);
            await _uow.Repository<Category>().AddAsync(ent);
            await _uow.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(ent);
        }

        public async Task UpdateAsync(CategoryDto dto)
        {
            var repo = _uow.Repository<Category>();
            var ent  = await repo.GetByIdAsync(dto.Id);
            ent.ChangeName(dto.Name);
            repo.Update(ent);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var repo = _uow.Repository<Category>();
            var ent  = await repo.GetByIdAsync(id);
            repo.Remove(ent);
            await _uow.SaveChangesAsync();
        }
    }
}
