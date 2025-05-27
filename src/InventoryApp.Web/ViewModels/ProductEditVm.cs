using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InventoryApp.Application.DTOs;

namespace InventoryApp.Web.ViewModels
{
    public class ProductEditVm
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public ProductEditVm() { }
        public ProductEditVm(ProductDto dto)
        {
            Id          = dto.Id;
            Name        = dto.Name;
            Description = dto.Description;
            Price       = dto.Price;
        }

        public ProductDto ToDto()
            => new() { Id = Id, Name = Name, Description = Description, Price = Price };
    }
}