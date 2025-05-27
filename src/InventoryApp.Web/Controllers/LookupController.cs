using System.Linq;
using System.Threading.Tasks;
using InventoryApp.Application.DTOs;
using InventoryApp.Application.Services;
using InventoryApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Web.Controllers
{
    public class LookupController : Controller
    {
        private readonly ISupplierService _supplierSvc;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        public LookupController(
            ISupplierService supplierSvc,
            IProductService productService,
            IInventoryService inventoryService)
        {
            _supplierSvc = supplierSvc;
            _productService = productService;
            _inventoryService = inventoryService;
        }

        
        // SUPPLIERS
        

        public async Task<IActionResult> Suppliers()
        {
            var suppliers = await _supplierSvc.GetAllAsync();
            return View(suppliers);
        }

        public async Task<IActionResult> EditSupplier(int? id)
        {
            if (id == null || id == 0)
                return View(new SupplierEditVm());

            var dto = await _supplierSvc.GetByIdAsync(id.Value);
            if (dto == null) return NotFound();

            return View(new SupplierEditVm { Id = dto.Id, Name = dto.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSupplier(SupplierEditVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var dto = new SupplierDto { Id = vm.Id, Name = vm.Name };
            if (vm.Id == 0)
                await _supplierSvc.CreateAsync(dto);
            else
                await _supplierSvc.UpdateAsync(dto);

            return RedirectToAction(nameof(Suppliers));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            await _supplierSvc.DeleteAsync(id);
            return RedirectToAction(nameof(Suppliers));
        }

        
        // PRODUCTS
        

        public async Task<IActionResult> Products(string search, int page = 1)
        {
            var dtos = await _productService.GetAllAsync();
            var vm = new ProductListVm(dtos, search, page);
            return View("Products/Index", vm);
        }

        public async Task<IActionResult> EditProduct(int? id)
        {
            ProductDto dto;

            if (id == null || id == 0)
            {
                dto = new ProductDto();
            }
            else
            {
                dto = await _productService.GetByIdAsync(id.Value);
                if (dto == null) return NotFound();
            }

            var vm = new ProductEditVm(dto);
            return View("Products/Edit", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(ProductEditVm vm)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage);
                var allErrors = string.Join("; ", errors);
                System.Diagnostics.Debug.WriteLine("ModelState errors: " + allErrors);

                return View("Products/Edit", vm);
            }

            if (vm.Id == 0)
                await _productService.CreateAsync(vm.ToDto());
            else
                await _productService.UpdateAsync(vm.ToDto());

            return RedirectToAction(nameof(Products));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Products));
        }
    }
}
