using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InventoryApp.Application.DTOs;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces;
using InventoryApp.Domain.ValueObjects;
using BCrypt.Net;

namespace InventoryApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _uow.Repository<User>().GetAllAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var u = await _uow.Repository<User>().GetByIdAsync(id);
            return _mapper.Map<UserDto>(u);
        }

        public async Task<UserDto> CreateAsync(UserDto dto, string password)
        {
            var email = EmailAddress.Create(dto.Email);
            var hash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User(
                Guid.NewGuid(),
                dto.FirstName,
                dto.LastName,
                email,
                hash);

            await _uow.Repository<User>().AddAsync(user);
            await _uow.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateAsync(UserDto dto)
        {
            var u = await _uow.Repository<User>().GetByIdAsync(dto.Id);
            u = new User(u.Id, dto.FirstName, dto.LastName, EmailAddress.Create(dto.Email), u.PasswordHash);
            _uow.Repository<User>().Update(u);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var u = await _uow.Repository<User>().GetByIdAsync(id);
            _uow.Repository<User>().Remove(u);
            await _uow.SaveChangesAsync();
        }
    }
}
