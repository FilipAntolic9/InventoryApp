using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryApp.Application.Services;
using InventoryApp.Web.ViewModels;

namespace InventoryApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        public ProductsController(IProductService productService, IInventoryService inventoryService)
        {
            _productService = productService;
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> Index(string search, int page = 1)
        {
            var dtos = await _productService.GetAllAsync();
            var vm = new ProductListVm(dtos, search, page);
            return View(vm);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var dto = id == Guid.Empty
                ? new InventoryApp.Application.DTOs.ProductDto()
                : await _productService.GetByIdAsync(id);
            var vm = new ProductEditVm(dto);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (vm.Id == Guid.Empty)
                await _productService.CreateAsync(vm.ToDto());
            else
                await _productService.UpdateAsync(vm.ToDto());

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}